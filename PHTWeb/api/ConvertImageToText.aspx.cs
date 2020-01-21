using System;
using System.Drawing;
//using Tesseract;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PHTWeb.api
{
    public partial class ConvertImageToText : System.Web.UI.Page
    {


        protected Panel inputPanel;
        protected HtmlInputFile imageFile;
        protected HtmlButton submitFile;

        protected override void OnInit(EventArgs e)
        {
            this.submitFile.ServerClick += OnSubmitFileClicked;
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ParseNumbersWithTessnet2();
        }

        private void OnSubmitFileClicked(object sender, EventArgs args)
        {
            //using (var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.Default))
            //{
            //    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            //    //engine.SetVariable("tessedit_char_whitelist", "e0123456789");
            //    //engine.SetVariable("edges_use_new_outline_complexity", true);
            //    engine.SetVariable("tessedit_unrej_any_wd", true);
            //    engine.SetVariable("tessedit_fix_fuzzy_spaces", false);

            //    engine.TryPrintVariablesToFile(Server.MapPath(@"~/TessVariables.txt"));

            //    // have to load Pix via a bitmap since Pix doesn't support loading a stream.
            //    using (var image = new System.Drawing.Bitmap(imageFile.PostedFile.InputStream))
            //    {
            //        using (var pix = PixConverter.ToPix(image))
            //        {
            //            using (var page = engine.Process(pix, PageSegMode.SingleBlock))
            //            {
            //                Response.Write(page.GetMeanConfidence());
            //                Response.Write("</BR><PRE>");
            //                using (var lines = page.GetIterator())
            //                {
            //                    do
            //                    {
            //                        Response.Write(lines.GetText(PageIteratorLevel.TextLine));
            //                        //Response.Write("</BR>");
            //                    } while (lines.Next(PageIteratorLevel.TextLine));
            //                }
            //                Response.Write("</PRE>");
            //            }
            //        }
            //    }
            //}
        }

        private void ParseNumbersWithTessnet2()
        {
            //using (var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.Default))
            //{
            //    //engine.SetVariable("tessedit_char_whitelist", "e0123456789");
            //    var path  = 
            //    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            //    //engine.SetVariable("tessedit_fix_hyphens", 0);
            //    //tesseract->SetVariable("language_model_penalty_non_freq_dict_word", "0");
            //    //tesseract->SetVariable("language_model_penalty_non_dict_word", "0");
            //    //tesseract->SetVariable("tessedit_char_blacklist", "xyz");
            //    //tesseract->SetVariable("classify_bln_numeric_mode", "1");

            //    // have to load Pix via a bitmap since Pix doesn't support loading a stream.
            //    using (var image = new System.Drawing.Bitmap(Server.MapPath(@"~/OCR_TextTest.png")))
            //    {
            //        using (var pix = PixConverter.ToPix(image))
            //        {
            //            using (var page = engine.Process(pix, PageSegMode.SingleBlock))
            //            {
            //                Response.Write(page.GetMeanConfidence());
            //                Response.Write("</BR>");
            //                using (var lines = page.GetIterator())
            //                {
            //                    do
            //                    {
            //                        Response.Write(lines.GetText(PageIteratorLevel.TextLine));
            //                        Response.Write("</BR>");
            //                    } while (lines.Next(PageIteratorLevel.TextLine));
            //                }
            //                //Response.Write(page.GetMeanConfidence());
            //                //Response.Write("</BR>");
            //                //Response.Write(page.GetText());
            //                //engine.TryPrintVariablesToFile(Server.MapPath(@"~/TessVariables.txt"));
            //            }
            //        }

            //    }

            //}
        }

        private void ParseWithIronOcr()
        {
            //var Ocr = new IronOcr.AdvancedOcr()
            //{
            //    //                AcceptedOcrCharacters = "ABCDEFGHIJKLMNOPQRSTUVWX",
            //    AcceptedOcrCharacters = "1234567890",
            //    Language = IronOcr.Languages.English.OcrLanguagePack,
            //    ColorSpace = IronOcr.AdvancedOcr.OcrColorSpace.Color,
            //    ColorDepth = 0,
            //    EnhanceResolution = false,
            //    EnhanceContrast = false,
            //    CleanBackgroundNoise = false,
            //    RotateAndStraighten = false,
            //    DetectWhiteTextOnDarkBackgrounds = false,
            //    ReadBarCodes = false,
            //    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Fast,
            //    InputImageType = IronOcr.AdvancedOcr.InputTypes.Document
            //};
            //var Result = Ocr.Read(Server.MapPath(@"~/OCR_NumTest.png"));
            //for (var i = 0; i < Result.Pages.Count; i++)
            //{
            //    var page = Result.Pages[i];
            //    for (var j = 0; j < page.LinesOfText.Length; j++)
            //    {
            //        Response.Write(page.LinesOfText[j].Text);
            //        Response.Write("</BR>");
            //    }
            //}
        }
    }
}