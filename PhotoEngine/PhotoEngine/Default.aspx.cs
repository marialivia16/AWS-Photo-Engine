using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoEngine
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPreview_OnClick(object sender, EventArgs e)
        {
            panelPreview.Visible = false;
            Session["Image"] = fileUploader.PostedFile;
            var fs = fileUploader.PostedFile.InputStream;
            var br = new BinaryReader(fs);
            var bytes = br.ReadBytes((Int32)fs.Length);
            var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgPreview.ImageUrl = "data:image/png;base64," + base64String;
            imgPreview.Height = 200;
            panelPreview.Visible = true;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}