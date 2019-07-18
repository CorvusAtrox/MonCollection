using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;

namespace MonCollection
{
    public partial class FormNiqCalc : Form
    {

        private List<PKM> PkmDB;
        private int ind = 0;
        private string game;

        public FormNiqCalc()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            ComboBox[] cbs =
            {
                comboBoxSpecies
            };

            List <ComboBox> moveBoxes = new List<ComboBox>{ comboBoxMove1, comboBoxMove2, comboBoxMove3, comboBoxMove4 };

            foreach (var cb in cbs.Concat(moveBoxes))
            {
                cb.DisplayMember = nameof(ComboItem.Text);
                cb.ValueMember = nameof(ComboItem.Value);
            }

            var source = GameInfo.FilteredSources;

            comboBoxSpecies.DataSource = new BindingSource(source.Species, null);
            foreach (var mb in moveBoxes)
                mb.DataSource = new BindingSource(source.Moves, null);
        }

        public void loadDB(List<PKM> data, int index)
        {
            PkmDB = data;
            ind = index;
            game = data[index].Identifier.Split('\\')[1];
        }

        public void showValues()
        {
            PKM mon = PkmDB[ind];

            labelName.Text = String.Format("Name: {0}",mon.Nickname);
            labelGame.Text = String.Format("Game: {0}",game);

            comboBoxSpecies.SelectedValue = mon.Species;

            var query1 = PkmDB.Where(pk => pk.Species == mon.Species);
            var query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
            var query3 = query1.Where(pk => pk.Identifier.Split('\\')[1] == game);

            labelSpVal.Text = String.Format("{0} {1} {2}",query1.Count(), query2.Count(), query3.Count());

            comboBoxMove1.SelectedValue = mon.Move1;
            comboBoxMove2.SelectedValue = mon.Move2;
            comboBoxMove3.SelectedValue = mon.Move3;
            comboBoxMove4.SelectedValue = mon.Move4;

            List<Label> moveLabels = new List<Label> { labelMove1, labelMove2, labelMove3, labelMove4 };

            IEnumerable<PKM> query1a;
            IEnumerable<PKM> query2a;
            IEnumerable<PKM> query3a;

            if(mon.Move1 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move1 || pk.Move2 == mon.Move1 || pk.Move3 == mon.Move1 || pk.Move4 == mon.Move1);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Identifier.Split('\\')[1] == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove1.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move2 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move2 || pk.Move2 == mon.Move2 || pk.Move3 == mon.Move2 || pk.Move4 == mon.Move2);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Identifier.Split('\\')[1] == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove2.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move3 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move3 || pk.Move2 == mon.Move3 || pk.Move3 == mon.Move3 || pk.Move4 == mon.Move3);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Identifier.Split('\\')[1] == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove3.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }

            if (mon.Move4 > 0)
            {
                query1 = PkmDB.Where(pk => pk.Move1 == mon.Move4 || pk.Move2 == mon.Move4 || pk.Move3 == mon.Move4 || pk.Move4 == mon.Move4);
                query1a = query1.Where(pk => pk.Species == mon.Species);
                query2 = query1.Where(pk => getGen(pk.Identifier) == getGen(mon.Identifier));
                query2a = query2.Where(pk => pk.Species == mon.Species);
                query3 = query1.Where(pk => pk.Identifier.Split('\\')[1] == game);
                query3a = query3.Where(pk => pk.Species == mon.Species);

                labelMove4.Text = String.Format("({0} {1}) ({2} {3}) ({4} {5})"
                                                , query1.Count(), query1a.Count(), query2.Count()
                                                , query2a.Count(), query3.Count(), query3a.Count());
            }
        }

        private int getGen(string identifier)
        {
            string sub = identifier.Substring(identifier.IndexOf(".p"));
            return int.Parse(sub.Substring(3));
        }
    }
}
