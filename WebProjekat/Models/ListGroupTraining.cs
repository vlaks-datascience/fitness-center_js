using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class ListGroupTraining
    {

        public static GroupTraining UpdateGroupTraining(GroupTraining groupTraining)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/GroupTrainings.xml");
            groupTraining.FitnessCenter = null;
            groupTraining.VisitorsListing = null;
            foreach (XmlNode gts in xdc.SelectNodes("//GroupTrainings/GroupTraining"))
            {
                if (gts.SelectSingleNode("Name").InnerText == groupTraining.Name)
                {
                    gts.SelectSingleNode("TrainingType").InnerText = groupTraining.TrainingType;
                    gts.SelectSingleNode("TrainingDuration").InnerText = groupTraining.TrainingDuration.ToString();
                    gts.SelectSingleNode("MaxVisitors").InnerText = groupTraining.MaxVisitors.ToString();
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/GroupTrainings.xml");
                    return groupTraining;
                }
            }
            return null;
        }

        public static string CHECKTAJM(string TAJM)
        {
            if(DateTime.Parse(TAJM) < DateTime.Now)
            {
                return "yes";
            } else
            {
                return "no";
            }
        }

        public static GroupTraining Register(GroupTraining groupTraining)
        {
            // 1. dodaj fitnes centar u gt
            // 2. dodaj grupni kod loggera
            // 3. update loggera u xml
            // 3. 
            
            foreach (GroupTraining gt in LoadAllGTS())
            {
                // provera da li vec postoji grupni trening sa istim imenom
                if(groupTraining.Name == gt.Name)
                {
                    return null;
                }
                // provera da li je najmanje 3 dana unapred
                if(groupTraining.DateNTime < DateTime.Now.AddDays(3))
                {
                    return null;
                }
                // 1.
                groupTraining.FitnessCenter = ListUser.Logger.FitnessCenterCoach;

                // 2.
                groupTraining.VisitorsListing = new List<User>();
                //ListUser.Logger.GroupTrainingsCoach.Add(groupTraining);

                // 3.
                XmlDocument xdc = new XmlDocument();
                xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
                //List<GroupTraining> AllGroupTrainings = new List<GroupTraining>();
                int xyz = 0;
                foreach (XmlNode users in xdc.SelectNodes("//Coaches/User"))
                {
                    if(users.SelectSingleNode("Username").InnerText == ListUser.Logger.Username)
                    {
                        XmlDocument documente = new XmlDocument();
                        string filepatho = HostingEnvironment.MapPath("~/App_Data/Coaches.xml");
                        documente.Load(filepatho);
                        var rootNode_o = documente.GetElementsByTagName("GroupTrainingsCoach")[xyz];
                        var nav_o = rootNode_o.CreateNavigator();
                        var emptyNamespaces_o = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                        using (var writer_o = nav_o.AppendChild())
                        {
                            var serializer_o = new XmlSerializer(groupTraining.GetType());
                            writer_o.WriteWhitespace("");
                            serializer_o.Serialize(writer_o, groupTraining, emptyNamespaces_o);
                            writer_o.Close();
                        }
                        documente.Save(filepatho);

                        XmlDocument xmldocumente = new XmlDocument();
                        string xmlfilepatho = HostingEnvironment.MapPath("~/App_Data/GroupTrainings.xml");
                        xmldocumente.Load(xmlfilepatho);
                        var xmlrootNode_o = xmldocumente.GetElementsByTagName("GroupTrainings")[0];
                        var xmlnav_o = xmlrootNode_o.CreateNavigator();
                        var xmlemptyNamespaces_o = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                        using (var xmlwriter_o = xmlnav_o.AppendChild())
                        {
                            var xmlserializer_o = new XmlSerializer(groupTraining.GetType());
                            xmlwriter_o.WriteWhitespace("");
                            xmlserializer_o.Serialize(xmlwriter_o, groupTraining, xmlemptyNamespaces_o);
                            xmlwriter_o.Close();
                        }
                        xmldocumente.Save(xmlfilepatho);

                        return groupTraining;
                    }
                    xyz++;
                }
            }
            return null;
        }

        public static List<GroupTraining> LoadAllGTS()
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/GroupTrainings.xml");
            List<GroupTraining> AllGroupTrainings = new List<GroupTraining>();
            foreach (XmlNode gts in xdc.SelectNodes("//GroupTrainings/GroupTraining"))
            {
                string name = gts.SelectSingleNode("Name").InnerText;
                string trainingType = gts.SelectSingleNode("TrainingType").InnerText;
                string FCname = "";
                string FCaddress = "";
                string FCowner = "";
                foreach (XmlNode fcs in gts.SelectNodes("FitnessCenter"))
                {
                    FCname = fcs.SelectSingleNode("Name").InnerText;
                    FCaddress = fcs.SelectSingleNode("Address").InnerText;
                    FCowner = fcs.SelectSingleNode("Owner").SelectSingleNode("Name").InnerText;
                }
                double trainingDuration = Convert.ToDouble(gts.SelectSingleNode("TrainingDuration").InnerText);
                DateTime dateandtime = DateTime.Parse(gts.SelectSingleNode("DateNTime").InnerText);
                int maxvisitors = Int32.Parse(gts.SelectSingleNode("MaxVisitors").InnerText);
                List<User> visitori = new List<User>();
                foreach (XmlNode userslis in gts.SelectSingleNode("VisitorsListing").SelectNodes("User"))
                {
                    //if (userslis.HasChildNodes)
                    //{

                        visitori.Add(new User() { Username = userslis.SelectSingleNode("Username").InnerText });
                    //}      
                }
                AllGroupTrainings.Add(new GroupTraining() { Name = name, TrainingType = trainingType, FitnessCenter = new FitnessCenter() { Name = FCname, Address = FCaddress, Owner = new User() { Username = FCowner } }, TrainingDuration = trainingDuration, DateNTime = dateandtime, MaxVisitors = maxvisitors, VisitorsListing = visitori });
            }
            return AllGroupTrainings;
        }
        
        public static List<GroupTraining> GTforCoach()
        {
            List<string> nazivi = new List<string>();
            List<GroupTraining> gtsForCoach = new List<GroupTraining>();
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
            foreach (XmlNode users in xdc.SelectNodes("//Coaches/User"))
            {
                if(ListUser.Logger.Username == users.SelectSingleNode("Username").InnerText)
                {
                    foreach (XmlNode gts in users.SelectSingleNode("GroupTrainingsCoach").SelectNodes("GroupTraining"))
                    {
                        nazivi.Add(gts.InnerText);
                    }
                    foreach(GroupTraining groupTraining in LoadAllGTS())
                    {
                        foreach(string n in nazivi)
                        {
                            if(groupTraining.Name == n)
                            {
                                gtsForCoach.Add(groupTraining);
                            }
                        }
                    }
                    return gtsForCoach;
                }
            }
            return null;
        }

        public static string Dodaj(string Name) {
            int i = 0;
            GroupTraining zadodavanjegt = new GroupTraining();
            //GroupTraining grupniZaUsera = new GroupTraining();
            foreach(GroupTraining gt in LoadAllGTS())
            {
                zadodavanjegt = gt;
                // AKO JE TO TAJ GRUPNI TRENING SA TIM IMENOM, AKO NIJE JOS GOTOV TRENING, AKO IMA MESTA ZA NOVOG
                if(gt.Name == Name && gt.DateNTime > DateTime.Now && gt.VisitorsListing.Count < gt.MaxVisitors)
                {
                    foreach(User u in gt.VisitorsListing)
                    {
                        // AKO USERNAME OD ULOGOVANOG VEC POSTOJI U LISTI VISITORA
                        if(u.Username == ListUser.Logger.Username)
                        {
                            return "Vec ste prijavljeni za ovaj termin!";
                        }
                    }
                    // IMPLEMENTIRAJ DODAVANJE
                    XmlDocument docbato = new XmlDocument();
                    string filepath = HostingEnvironment.MapPath("~/App_Data/GroupTrainings.xml");
                    docbato.Load(filepath);
                    var rootNode = docbato.GetElementsByTagName("VisitorsListing")[i];
                    var nav = rootNode.CreateNavigator();
                    var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                    using (var writer = nav.AppendChild())
                    {
                        var serializer = new XmlSerializer(ListUser.Logger.GetType());
                        writer.WriteWhitespace("");
                        serializer.Serialize(writer, ListUser.Logger, emptyNamespaces);
                        writer.Close();
                    }
                    docbato.Save(filepath);
                    //grupniZaUsera = gt;
                    // dodaj logiku za dodavanje treninga kod visitora na koje je prijavljen
                    XmlDocument xdc = new XmlDocument();
                    xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Visitors.xml");
                    int y = 0;
                    foreach (XmlNode gts in xdc.SelectNodes("//Visitors/User"))
                    {
                        if(gts.SelectSingleNode("Username").InnerText == ListUser.Logger.Username)
                        {
                            XmlDocument zadodavanje_docbato = new XmlDocument();
                            string zadodavanje_filepath = HostingEnvironment.MapPath("~/App_Data/Visitors.xml");
                            zadodavanje_docbato.Load(zadodavanje_filepath);
                            var zadodavanje_rootNode = zadodavanje_docbato.GetElementsByTagName("GroupTrainingsVisitor")[y];
                            var zadodavanje_nav = zadodavanje_rootNode.CreateNavigator();
                            var zadodavanje_emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                            using (var zadodavanje_writer = zadodavanje_nav.AppendChild())
                            {
                                var zadodavanje_serializer = new XmlSerializer(zadodavanjegt.GetType()); // ovde grupni trening
                                zadodavanje_writer.WriteWhitespace("");
                                zadodavanje_serializer.Serialize(zadodavanje_writer, zadodavanjegt, zadodavanje_emptyNamespaces); // ovde grupni trening
                                zadodavanje_writer.Close();
                            }
                            zadodavanje_docbato.Save(zadodavanje_filepath);
                            return "Successfully applied for visiting!";
                        }
                        y++;
                    }
                    return "Successfully applied for visiting!";
                }
                i++;
            }
            return null;
        }
        
        public static List<GroupTraining> GetAllGTS(string vreme)
        {
            List<GroupTraining> gctime = new List<GroupTraining>();
            foreach (var item in LoadAllGTS())
            {
                if (item.DateNTime > DateTime.Now)
                {
                    gctime.Add(item);
                }
            }
            return gctime;
        } 

        public static List<GroupTraining> GetPreviousTrainings()
        {
            List<GroupTraining> groupTrainings = LoadAllGTS();
            List<GroupTraining> dodaj = new List<GroupTraining>();
            foreach (GroupTraining grtr in groupTrainings)
            {
                if(grtr.DateNTime < DateTime.Now)
                {
                    foreach(User us in grtr.VisitorsListing)
                    {
                        if(us.Username == ListUser.Logger.Username)
                        {
                            dodaj.Add(grtr);
                        }
                    }
                }
            }
            return dodaj;
        }

        public static List<GroupTraining> SortingPrevious(string Name, string TrainingType, string FCName)
        {
            List<GroupTraining> zaSlanje = new List<GroupTraining>();
            // SAMO IME UNETO
            if(Name != null && TrainingType == null && FCName == null)
            {
                foreach(GroupTraining gt in GetPreviousTrainings())
                {
                    if(Name == gt.Name)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje; 
            }
            // SAMO TRENING UNET
            else if (Name == null && TrainingType != null && FCName == null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (TrainingType == gt.TrainingType)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // SAMO FITNES UNET
            else if (Name == null && TrainingType == null && FCName != null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (FCName == gt.FitnessCenter.Name)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - IME I TRENING UNETI
            else if (Name != null && TrainingType != null && FCName == null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (Name == gt.Name && TrainingType == gt.TrainingType)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - IME I FCNAME UNETI
            else if (Name != null && TrainingType == null && FCName != null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (Name == gt.Name && FCName == gt.FitnessCenter.Name)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - TRENING I FCNAME UNETI
            else if (Name == null && TrainingType != null && FCName != null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (TrainingType == gt.TrainingType && FCName == gt.FitnessCenter.Name)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // TRI - SVI UNETI
            else if (Name != null && TrainingType != null && FCName != null)
            {
                foreach (GroupTraining gt in GetPreviousTrainings())
                {
                    if (Name == gt.Name && FCName == gt.FitnessCenter.Name && TrainingType == gt.TrainingType)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            } else
            {
                return null;
            }
        }

        public static List<GroupTraining> SortingPersonal(string Name, string TrainingType, string GTmin, string GTmax)
        {
            List<GroupTraining> zaSlanje = new List<GroupTraining>();
            // SAMO IME UNETO
            if (Name != null && TrainingType == null && GTmin == "not" && GTmax == "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (Name == gt.Name && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // SAMO TRENING UNET
            else if (Name == null && TrainingType != null && GTmin == "not" && GTmax == "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (TrainingType == gt.TrainingType && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // SAMO FITNES UNET
            else if (Name == null && TrainingType == null && GTmin != "not" && GTmax != "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (DateTime.Parse(GTmin) < gt.DateNTime && DateTime.Parse(GTmax) > gt.DateNTime && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - IME I TRENING UNETI
            else if (Name != null && TrainingType != null && GTmin == "not" && GTmax == "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (Name == gt.Name && TrainingType == gt.TrainingType && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - IME I FCNAME UNETI
            else if (Name != null && TrainingType == null && GTmin != "not" && GTmax != "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (Name == gt.Name && DateTime.Parse(GTmin) < gt.DateNTime && DateTime.Parse(GTmax) > gt.DateNTime && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // DVA - TRENING I FCNAME UNETI
            else if (Name == null && TrainingType != null && GTmin != "not" && GTmax != "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (TrainingType == gt.TrainingType && DateTime.Parse(GTmin) < gt.DateNTime && DateTime.Parse(GTmax) > gt.DateNTime && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            // TRI - SVI UNETI
            else if (Name != null && TrainingType != null && GTmin != "not" && GTmax != "not")
            {
                foreach (GroupTraining gt in GTforCoach())
                {
                    if (Name == gt.Name && DateTime.Parse(GTmin) < gt.DateNTime && DateTime.Parse(GTmax) > gt.DateNTime && TrainingType == gt.TrainingType && gt.DateNTime < DateTime.Now)
                    {
                        zaSlanje.Add(gt);
                    }
                }
                return zaSlanje;
            }
            else
            {
                return null;
            }
        }
    }
}