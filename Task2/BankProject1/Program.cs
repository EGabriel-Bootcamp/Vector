// See https://aka.ms/new-console-template for more information
using BankProjectTask2Services;

Console.WriteLine("Hello, World!");



UserServices userServices = new UserServices();
userServices.InitializeData();
//userServices.Run();
//userServices.Regiser();
userServices.OnBoard();






//decimal val = 1278.112m;


//CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
//Console.WriteLine($"{val}");
//var culture = CultureInfo.CreateSpecificCulture("en-US");
//var culture2 = new CultureInfo("en-US");

//string numberAsString = val.ToString("# #####.###", culture);
//string numberAsString2 = val.ToString("# #####.###", culture2);
//Console.WriteLine(numberAsString);
//Console.WriteLine(numberAsString2);

//var culture = CultureInfo.CreateSpecificCulture("en-US");
//var culture = CultureInfo.CreateSpecificCulture("en-NG");
//Console.OutputEncoding = Encoding.Unicode;
//var culture = CultureInfo.CreateSpecificCulture("yo-NG");
////decimal number = 1_500_000.50m;
//decimal number = 1500000.50m;

//string numberString = number.ToString("C", CultureInfo.InvariantCulture);
////string numberString = number.ToString("C");

//string numberStringTwo =  number.ToString("C0", culture);
//Console.WriteLine(numberString);
//Console.WriteLine(numberStringTwo);