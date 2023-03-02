// See https://aka.ms/new-console-template for more information

using BankProject.AppServices;
using BankProject.Display;
using System.Globalization;
using System.Text;



UserServices userServices = new UserServices();
userServices.InitializeData();
//userServices.Run();
//userServices.Regiser();
userServices.OnBoard();













