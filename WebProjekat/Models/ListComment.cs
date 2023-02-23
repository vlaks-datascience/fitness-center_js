using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class ListComment
    {
        public static List<Comment> LoadAllCOM()
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Comments.xml");
            List<Comment> AllComments = new List<Comment>();
            foreach (XmlNode fcs in xdc.SelectNodes("//Comments/Comment"))
            {
                string username = fcs.SelectSingleNode("ReviewVisitor").SelectSingleNode("Username").InnerText;
                string name = fcs.SelectSingleNode("ReviewFitnessCenter").SelectSingleNode("Name").InnerText;
                string address = fcs.SelectSingleNode("ReviewFitnessCenter").SelectSingleNode("Address").InnerText;
                string comment = fcs.SelectSingleNode("ReviewVisitorsComment").InnerText;
                int rating = Int32.Parse(fcs.SelectSingleNode("ReviewRating").InnerText);
                string accepted = fcs.SelectSingleNode("Accepted").InnerText;
                AllComments.Add(new Comment() { ReviewVisitor = new User() { Username = username }, ReviewFitnessCenter = new FitnessCenter() { Name = name, Address = address }, ReviewVisitorsComment = comment, ReviewRating = rating, Accepted = accepted });
            }
            return AllComments;
        }

        public static bool Confirm(string Nameu, string Namef, string Commentf)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Comments.xml");
            foreach (XmlNode fcs in xdc.SelectNodes("//Comments/Comment"))
            {
                string usernf = fcs.SelectSingleNode("ReviewVisitor").SelectSingleNode("Username").InnerText;
                string namff = fcs.SelectSingleNode("ReviewFitnessCenter").SelectSingleNode("Name").InnerText;
                string commenff = fcs.SelectSingleNode("ReviewVisitorsComment").InnerText;
                string isApproved = fcs.SelectSingleNode("Accepted").InnerText;

                if (usernf == Nameu && namff == Namef && commenff == Commentf && isApproved == "WaitingApprove")
                {
                    fcs.SelectSingleNode("Accepted").InnerText = "Yes";
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Comments.xml");
                    return true;
                }
            }
            return false;
        }

        public static bool DenyComment(string Nameu, string Namef, string Commentf)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Comments.xml");
            foreach (XmlNode fcs in xdc.SelectNodes("//Comments/Comment"))
            {
                string usernf = fcs.SelectSingleNode("ReviewVisitor").SelectSingleNode("Username").InnerText;
                string namff = fcs.SelectSingleNode("ReviewFitnessCenter").SelectSingleNode("Name").InnerText;
                string commenff = fcs.SelectSingleNode("ReviewVisitorsComment").InnerText;
                string isApproved = fcs.SelectSingleNode("Accepted").InnerText;

                if (usernf == Nameu && namff == Namef && commenff == Commentf && isApproved == "WaitingApprove")
                {
                    fcs.SelectSingleNode("Accepted").InnerText = "No";
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Comments.xml");
                    return true;
                }
            }
            return false;
        }

        public static List<Comment> Approve()
        {
            List<Comment> zaDodaju = new List<Comment>();
            foreach(Comment com in LoadAllCOM())
            {
                if(com.Accepted == "WaitingApprove")
                {
                    zaDodaju.Add(com);
                }
            }
            return zaDodaju;
        }

        //public static List<Comment> CommentsList { get; set; } = LoadAllCOM();
        public static bool LeaveComment(string Komentar, string Ocena, string Naziv, string NazivFC)
        {
            XmlDocument docbato = new XmlDocument();
            string filepath = HostingEnvironment.MapPath("~/App_Data/Comments.xml");
            docbato.Load(filepath);
            var rootNode = docbato.GetElementsByTagName("Comments")[0];
            var nav = rootNode.CreateNavigator();
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            foreach(FitnessCenter fc in ListFitnessCenter.LoadAllFCS())
            {
                if(fc.Name == NazivFC)
                {
                    Comment pomocni = new Comment() { ReviewVisitor = new User() { Username = ListUser.Logger.Username }, ReviewFitnessCenter = new FitnessCenter() { Name = NazivFC, Address = fc.Address }, ReviewVisitorsComment = Komentar, ReviewRating = Int32.Parse(Ocena), Accepted = "WaitingApprove" };
                    Comment cmzaubac = pomocni;
                    using (var writer = nav.AppendChild())
                    {
                        var serializer = new XmlSerializer(cmzaubac.GetType());
                        writer.WriteWhitespace("");
                        serializer.Serialize(writer, cmzaubac, emptyNamespaces);
                        writer.Close();
                    }
                    docbato.Save(filepath);
                    return true;
                }
            }
            return false;
        }

        public static List<Comment> GetFitnessByNameAddress(string Name, string Address)
        {
            List<Comment> lc = new List<Comment>();
            foreach (var item in LoadAllCOM())
            {
                if(item.ReviewFitnessCenter.Name == Name && item.ReviewFitnessCenter.Address == Address && item.Accepted == "Yes")
                {
                    lc.Add(item);
                }
            }
            return lc;
        }
    }
}