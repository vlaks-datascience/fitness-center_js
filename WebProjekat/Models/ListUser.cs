using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;
using WebProjekat.Models.Enums;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class ListUser
    {
        public static User Logger { get; set; } = new User();

        public static User UserLogged( string un, string pw, string nm, string sn, TypeSex sx, string em, DateTime dt, UserRoles rl, List<GroupTraining> gtVis, List<GroupTraining> gtCoa, FitnessCenter fcCoa, List<FitnessCenter> fcOwn)
        {
            return new User() { Username = un, Password = pw, Name = nm, Surname = sn, Sex = sx, Email = em, Date = dt, Role = rl, GroupTrainingsVisitor = gtVis, GroupTrainingsCoach = gtCoa, FitnessCenterCoach = fcCoa, FitnessCenterOwner = fcOwn };
        }
        public static User Nzm(string GetUser)
        {
            if(Brojac == 0)
            {
                return null;
            }
            if(Brojac == 1)
            {
                XmlDocument xml1 = new XmlDocument();
                xml1.Load("C:\\Users\\Vlaks\\source\\repos\\WebProjekat\\WebProjekat\\App_Data\\Visitors.xml");
                XmlNodeList xnList1 = xml1.SelectNodes("/Visitors/User");
                foreach (XmlNode xn1 in xnList1) {
                    string usernam = xn1["Username"].InnerText;
                    if (GetUser == usernam)
                    {
                        TypeSex se = TypeSex.Male;
                        string passwor = xn1["Password"].InnerText;
                        string nam = xn1["Name"].InnerText;
                        string surnam = xn1["Surname"].InnerText;
                        if(xn1["Sex"].InnerText == "Female")
                        {
                            se = TypeSex.Female;
                        }
                        string emai = xn1["Email"].InnerText;
                        string dat = xn1["Date"].InnerText;
                        DateTime dt = DateTime.Parse(dat);
                        UserRoles rol = UserRoles.Visitor;
                        List<GroupTraining> lgt = new List<GroupTraining>();
                        foreach(GroupTraining gtlista in ListGroupTraining.LoadAllGTS())
                        {
                            foreach(User u in gtlista.VisitorsListing)
                            {
                                if(u.Username == usernam)
                                {
                                    lgt.Add(gtlista);
                                }
                            }
                        }
                        return UserLogged(usernam, passwor, nam, surnam, se, emai, dt, rol, lgt, null, null, null);
                    }
                }
            } else if(Brojac == 2)
            {
                XmlDocument xml2 = new XmlDocument();
                xml2.Load("C:\\Users\\Vlaks\\source\\repos\\WebProjekat\\WebProjekat\\App_Data\\Coaches.xml");
                XmlNodeList xnList2 = xml2.SelectNodes("/Coaches/User");
                foreach (XmlNode xn2 in xnList2)
                {
                    string usernam = xn2["Username"].InnerText;
                    if (GetUser == usernam)
                    {
                        TypeSex se = TypeSex.Male;
                        string passwor = xn2["Password"].InnerText;
                        string nam = xn2["Name"].InnerText;
                        string surnam = xn2["Surname"].InnerText;
                        if (xn2["Sex"].InnerText == "Female")
                        {
                            se = TypeSex.Female;
                        }
                        string emai = xn2["Email"].InnerText;
                        string dat = xn2["Date"].InnerText;
                        DateTime dt = DateTime.Parse(dat);
                        UserRoles rol = UserRoles.Coach;
                        return UserLogged(usernam, passwor, nam, surnam, se, emai, dt, rol, null, null, null, null);
                    }
                }
            } else if(Brojac == 3) {
                XmlDocument xml3 = new XmlDocument();
                xml3.Load("C:\\Users\\Vlaks\\source\\repos\\WebProjekat\\WebProjekat\\App_Data\\Owners.xml");
                XmlNodeList xnList3 = xml3.SelectNodes("/Owners/User");
                foreach (XmlNode xn3 in xnList3)
                {
                    string usernam = xn3["Username"].InnerText;
                    if (GetUser == usernam)
                    {
                        TypeSex se = TypeSex.Male;
                        string passwor = xn3["Password"].InnerText;
                        string nam = xn3["Name"].InnerText;
                        string surnam = xn3["Surname"].InnerText;
                        if (xn3["Sex"].InnerText == "Female")
                        {
                            se = TypeSex.Female;
                        }
                        string emai = xn3["Email"].InnerText;
                        string dat = xn3["Date"].InnerText;
                        DateTime dt = DateTime.Parse(dat);
                        UserRoles rol = UserRoles.Owner;
                        return UserLogged(usernam, passwor, nam, surnam, se, emai, dt, rol, null, null, null, null);
                    }
                }
            }
            return null;
        }
        public static int Brojac { get; set; } = 0;
        private static readonly XmlDocument docbato = new XmlDocument();

        public static List<User> UsersList { get; set; } = new List<User>()
        {
            
        };

        public static User FindByUsername(string username)
        {
            return UsersList.Find(item => item.Username == username);
        }

        public static User AddUser(User user)
        {
            UsersList.Add(user);
            return user;
        }

        public static void RemoveUser(User user)
        {
            UsersList.Remove(user);
        }

        public static User UpdateVisitor(User user)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Visitors.xml");
            foreach (XmlNode users in xdc.SelectNodes("//Visitors/User"))
            {
                string username = users.SelectSingleNode("Username").InnerText;
                if (user.Username == username)
                {
                    users.SelectSingleNode("Password").InnerText = user.Password;
                    users.SelectSingleNode("Name").InnerText = user.Name;
                    users.SelectSingleNode("Surname").InnerText = user.Surname;
                    users.SelectSingleNode("Sex").InnerText = user.Sex.ToString();
                    users.SelectSingleNode("Email").InnerText = user.Email;
                    User zamena = (User)HttpContext.Current.Session["user"];
                    zamena.Password = user.Password;
                    zamena.Name = user.Name;
                    zamena.Surname = user.Surname;
                    zamena.Sex = user.Sex;
                    zamena.Email = user.Email;
                    HttpContext.Current.Session["user"] = zamena;
                    Logger = (User)HttpContext.Current.Session["user"];
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Visitors.xml");
                    return zamena;
                }
            }
            return null;
        }

        public static User UpdateCoach(User user)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
            foreach (XmlNode users in xdc.SelectNodes("//Coaches/User"))
            {
                string username = users.SelectSingleNode("Username").InnerText;
                if (user.Username == username)
                {
                    users.SelectSingleNode("Password").InnerText = user.Password;
                    users.SelectSingleNode("Name").InnerText = user.Name;
                    users.SelectSingleNode("Surname").InnerText = user.Surname;
                    users.SelectSingleNode("Sex").InnerText = user.Sex.ToString();
                    users.SelectSingleNode("Email").InnerText = user.Email;
                    User zamena = (User)HttpContext.Current.Session["user"];
                    zamena.Password = user.Password;
                    zamena.Name = user.Name;
                    zamena.Surname = user.Surname;
                    zamena.Sex = user.Sex;
                    zamena.Email = user.Email;
                    HttpContext.Current.Session["user"] = zamena;
                    Logger = (User)HttpContext.Current.Session["user"];
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
                    return zamena;
                }
            }
            return null;
        }

        public static User UpdateOwner(User user)
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Owners.xml");
            foreach (XmlNode users in xdc.SelectNodes("//Owners/User"))
            {
                string username = users.SelectSingleNode("Username").InnerText;
                if (user.Username == username)
                {
                    users.SelectSingleNode("Password").InnerText = user.Password;
                    users.SelectSingleNode("Name").InnerText = user.Name;
                    users.SelectSingleNode("Surname").InnerText = user.Surname;
                    users.SelectSingleNode("Sex").InnerText = user.Sex.ToString();
                    users.SelectSingleNode("Email").InnerText = user.Email;
                    User zamena = (User)HttpContext.Current.Session["user"];
                    zamena.Password = user.Password;
                    zamena.Name = user.Name;
                    zamena.Surname = user.Surname;
                    zamena.Sex = user.Sex;
                    zamena.Email = user.Email;
                    HttpContext.Current.Session["user"] = zamena;
                    Logger = (User)HttpContext.Current.Session["user"];
                    xdc.Save("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Owners.xml");
                    return zamena;
                }
            }
            return null;
        }



        public static User LoginCheck(string Username, string Password)
        {
            bool vla = false;
            bool pos = false;
            bool tre = false;
            bool exists = false;
            XDocument doc1 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Visitors.xml");
            var usernames1 = doc1.Descendants("Username");
            var passwords1 = doc1.Descendants("Password");
            List<string> AllUsernames = new List<string>();
            List<string> AllPasswords = new List<string>();
            foreach (var un in usernames1)
            {
                if(un.Value == Username) { pos = true; }
                AllUsernames.Add(un.Value);
            }
            foreach (var un in passwords1)
            {
                AllPasswords.Add(un.Value);
            }

            XDocument doc2 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Owners.xml");
            var usernames2 = doc2.Descendants("Username");
            var passwords2 = doc2.Descendants("Password");
            foreach (var un in usernames2)
            {
                if (un.Value == Username) { vla = true; }
                AllUsernames.Add(un.Value);
            }
            foreach (var un in passwords2)
            {
                AllPasswords.Add(un.Value);
            }

            XDocument doc3 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
            var usernames3 = doc3.Descendants("Username");
            var passwords3 = doc3.Descendants("Password");
            foreach (var un in usernames3)
            {
                if (un.Value == Username) { tre = true; }
                AllUsernames.Add(un.Value);
            }
            foreach (var un in passwords3)
            {
                AllPasswords.Add(un.Value);
            }

            foreach(var item in AllUsernames)
            {
                if(item == Username)
                {
                    exists = true;
                }
            }
            if (exists == false)
            {
                return null;
            }

            for (int i = 0; i <= AllUsernames.Count(); i++)
            {
                if(AllUsernames[i] == Username )
                {
                    if(AllPasswords[i] == Password)
                    {
                        if (pos)
                        {
                            Brojac = 1;
                            return Nzm(Username);
                        }
                        else if (tre)
                        {
                            Brojac = 2;
                            return Nzm(Username);
                        }
                        else if (vla)
                        {
                            Brojac = 3;
                            return Nzm(Username);
                        }
                    } else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public static User Register(User user)
        {
            // proveri da li vec postoji uneti username
            XDocument doc1 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Visitors.xml");
            var usernames1 = doc1.Descendants("Username");
            foreach (var un in usernames1)
            {
                if (un.Value == user.Username) { return null; }
            }

            // proveri da li vec postoji uneti username
            XDocument doc2 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Owners.xml");
            var usernames2 = doc2.Descendants("Username");
            foreach (var un in usernames2)
            {
                if (un.Value == user.Username) { return null; }
            }
            // proveri da li vec postoji uneti username
            XDocument doc3 = XDocument.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/Coaches.xml");
            var usernames3 = doc3.Descendants("Username");
            foreach (var un in usernames3)
            {
                if (un.Value == user.Username) { return null; }
            }

            // ako nije vratio null nastavlja dalje sa upisom usera u visitors xml fajl
            string filepath = HostingEnvironment.MapPath("~/App_Data/Visitors.xml");
            docbato.Load(filepath);
            var rootNode = docbato.GetElementsByTagName("Visitors")[0];
            var nav = rootNode.CreateNavigator();
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = nav.AppendChild())
            {
                user.GroupTrainingsVisitor = new List<GroupTraining>();
                var serializer = new XmlSerializer(user.GetType());
                writer.WriteWhitespace("");
                serializer.Serialize(writer, user, emptyNamespaces);
                writer.Close();
            }
            docbato.Save(filepath);

            return user;
        }
    }
}