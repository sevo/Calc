using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
    //
    //      Mozno to dam do kelu
    //
    public class ToolTipDB
    {
        private List<DBdata> items;

        public ToolTipDB()
        {
            items = new List<DBdata>();
        }

        public string this[System.Windows.Forms.Button ktory]
        {
            get
            {
                DBdata navrat = search(ktory.Text);
                if (navrat == null)
                {
                    return "*** NYI :( ***";
                }
                return navrat.Text;
            }
            set 
            {
                DBdata hodnota = search(ktory.Text);
                if (hodnota == null) return;
                hodnota.Text = value;
            }
        }

        DBdata search(string name)
        {
            foreach (DBdata d in items)
            {
                if (d.Name == name)
                {
                    return d;
                }
            }

            return null;
        }

        public void Add(string name, string text)
        {
            DBdata data=new DBdata(name, text);
            items.Add(data);
            
        }
    }


    class DBdata
    {

        public string Name { get; set; }
        public string Text { get; set; }

        public DBdata(string name,  string text) {
            Name = name;
            Text = text;
        }
    }
}
