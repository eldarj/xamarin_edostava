﻿using Easyfood_Xamarin.Helpers;
using Easyfood_Xamarin.Model;
using Easyfood_Xamarin.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Easyfood_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RestoraniOmiljeniPage : ContentPage
    {
        private WebApiHelper servis = new WebApiHelper("korisnici");
        private List<Restoran> listRestorani;

        public RestoraniOmiljeniPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            loadDataFromApi();
            base.OnAppearing();
        }

        private async void loadDataFromApi()
        {
            containerApiError.IsVisible = false;
            loaderIndicator.IsRunning = loaderIndicator.IsVisible = true;
            try
            {
                HttpResponseMessage response = await servis.GetResponse(Global.Narucilac.KorisnikID + "/omiljenirestorani");
                if (response.IsSuccessStatusCode)
                {
                    listRestorani = JsonConvert.DeserializeObject<List<Restoran>>(response.Content.ReadAsStringAsync().Result);
                    if (listRestorani.Count == 0)
                    {
                        listRestorani.Add(new Restoran { Naziv = "Nemate omiljenih restorana!" });
                    }
                    listViewRestorani.ItemsSource = listRestorani;
                }
                else
                {
                    containerApiError.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                containerApiError.IsVisible = true;
            }
            loaderIndicator.IsRunning = loaderIndicator.IsVisible = false;
        }

        private void ListViewRestorani_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null && (e.Item as Restoran).RestoranID != 0)
            {
                Navigation.PushAsync(new RestoraniDetailsPage(e.Item as Restoran));
            }
        }

        private void BtnReloadPodatke_Clicked(object sender, EventArgs e)
        {
            loadDataFromApi();
        }
    }
}