using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Xml;

namespace WebProjekat.Models
{
    public class ListFitnessCenter
    {
        public static List<FitnessCenter> LoadAllFCS()
        {
            // ako nije vratio null nastavlja dalje sa upisom usera u visitors xml fajl
            XmlDocument xdc = new XmlDocument();
            xdc.Load("C:/Users/Vlaks/source/repos/WebProjekat/WebProjekat/App_Data/FitnessCenters.xml");
            List<FitnessCenter> AllFitnessCenters = new List<FitnessCenter>();
            foreach (XmlNode fcs in xdc.SelectNodes("//FitnessCenters/FitnessCenter"))
            {
                string name = fcs.SelectSingleNode("Name").InnerText;
                string address = fcs.SelectSingleNode("Address").InnerText;
                int established = Int32.Parse(fcs.SelectSingleNode("Established").InnerText);
                string username = "";
                foreach (XmlNode user in fcs.SelectNodes("Owner"))
                {
                    username = user.SelectSingleNode("Username").InnerText;
                }
                double monthcost = Convert.ToDouble(fcs.SelectSingleNode("MonthCost").InnerText);
                double yearcost = Convert.ToDouble(fcs.SelectSingleNode("YearCost").InnerText);
                double onecost = Convert.ToDouble(fcs.SelectSingleNode("OneCost").InnerText);
                double onegroupcost = Convert.ToDouble(fcs.SelectSingleNode("OneGroupCost").InnerText);
                double onewithcoachcost = Convert.ToDouble(fcs.SelectSingleNode("OneWithCoachCost").InnerText);

                AllFitnessCenters.Add(new FitnessCenter() { Name = name, Address = address, Established = established, Owner = new User() { Username = username }, MonthCost = monthcost, YearCost = yearcost, OneCost = onecost, OneGroupCost = onegroupcost, OneWithCoachCost = onewithcoachcost });
            }
            return AllFitnessCenters;
        }

        public static List<FitnessCenter> FcentersList { get; set; } = LoadAllFCS();

        public static FitnessCenter FindByName(string Name)
        {
            return FcentersList.Find(item => item.Name == Name);
        }

        public static List<FitnessCenter> FindByAddress(string Address)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Address == Address)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static List<FitnessCenter> FindByNames(string Name)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Name == Name)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static List<FitnessCenter> FindByEstablished(int Established)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Established == Established)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static List<FitnessCenter> FindByNameAddress(string Name, string Address)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Name == Name && item.Address == Address)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static List<FitnessCenter> FindByNameEstablished(string Name, int Established)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Name == Name && item.Established == Established)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static List<FitnessCenter> FindByAddressEstablished(string Address, int Established)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Address == Address && item.Established == Established)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }

        public static FitnessCenter FindByNameAddressSINGLE(string NameDET, string AddressDET)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if(item.Name == NameDET && item.Address == AddressDET)
                {
                    return item;
                }
            }
            return new FitnessCenter();
        }

        public static List<FitnessCenter> FindByAll (string name, string address, int established)
        {
            List<FitnessCenter> fc = new List<FitnessCenter>();
            foreach (var item in FcentersList)
            {
                if (item.Name == name && item.Address == address && item.Established == established)
                {
                    fc.Add(item);
                }
            }
            return fc;
        }


        //public static FitnessCenter AddFcenter(FitnessCenter fitnessCenter)
        //{
        //    FcentersList.Add(fitnessCenter);
        //    return fitnessCenter;
        //}

        //public static void RemoveFcenter(FitnessCenter fitnessCenter)
        //{
        //    FcentersList.Remove(fitnessCenter);
        //}

        //public static FitnessCenter UpdateFcenter(FitnessCenter fitnessCenter)
        //{
        //    FitnessCenter existingfCenter = FindByName(fitnessCenter.Name);
        //    existingfCenter.Name = fitnessCenter.Name;
        //    existingfCenter.Address = fitnessCenter.Address;
        //    existingfCenter.Established = fitnessCenter.Established;
        //    existingfCenter.Owner = fitnessCenter.Owner;
        //    existingfCenter.MonthCost = fitnessCenter.MonthCost;
        //    existingfCenter.YearCost = fitnessCenter.YearCost;
        //    existingfCenter.OneCost = fitnessCenter.OneCost;
        //    existingfCenter.OneGroupCost = fitnessCenter.OneGroupCost;
        //    existingfCenter.OneWithCoachCost = fitnessCenter.OneWithCoachCost;
        //    return fitnessCenter;
        //}
    }
}