﻿namespace F_V_Examen;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new CentroCostosPage());
    }
}
