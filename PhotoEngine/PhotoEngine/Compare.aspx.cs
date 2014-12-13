using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoEngine
{
    public partial class Compare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int n = ImageHash.H.Count;
            for (int i = 1; i < n; i++)
            {
                double percent = compareTwoImages(ImageHash.H.First().Value, ImageHash.H[i]);
                hash1.Text += (100 - percent) + "%, ";
            }
        }

        private double compareTwoImages(Hashtable h1, Hashtable h2)
        {
            double p = 0;
            foreach (DictionaryEntry entry in h1)
            {
                if (h2.Contains(entry.Key))
                {
                    p += Math.Abs(Convert.ToDouble(entry.Value) - Convert.ToDouble(h2[entry.Key]));
                }
                else
                {
                    p += Convert.ToDouble(entry.Value);
                }
            }
            return p;
        }
    }
}