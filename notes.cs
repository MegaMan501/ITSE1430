Notes: 

//	09/06/2017

	.Net is platform/libary, and managed programs use .Net; Like 
	Assembly 
	
	// Strings: 
	//Option 1
	string names = "John" + "William" + "Murphy" + "Charles" + "Henry";
				
	//Option 2
	StringBuilder builder = new StringBuilder();
	builder.Append("John");
	builder.Append("William");
	string name2 = builder.ToString();

	//Option 3
	string names3 = String.Concat("John" + "William" + "Murphy" + "Charles" + "Henry"); // String.Concat uses StringBuilder

//	09/11/2017

