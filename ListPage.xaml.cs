
using GeorgeaAdinaLab7;
using GeorgeaAdinaLab7.Models;

namespace GeorgeaAdinaLab7
{

    public partial class ListPage : ContentPage
    {
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }
        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)
           this.BindingContext)
            {
                BindingContext = new Product()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Ensure BindingContext is not null and is of type ShopList
            var shopl = BindingContext as ShopList;

            if (shopl != null)
            {
                listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
            }
            else
            {
                await DisplayAlert("Error", "No ShopList data found.", "OK");
            }
        }


        async void OnDeleteItemButtonClicked(object sender, EventArgs e)
        {
            
            var selectedProduct = listView.SelectedItem as Product;

            if (selectedProduct != null)
            {
               
                var listProductToDelete = new ListProduct
                {
                    ShopListID = ((ShopList)BindingContext).ID, 
                    ProductID = selectedProduct.ID
                };

               
                await App.Database.DeleteProductAsync(selectedProduct);

                
                listView.ItemsSource = await App.Database.GetListProductsAsync(((ShopList)BindingContext).ID);
            }
            else
            {
                
                await DisplayAlert("No Selection", "Please select a product to delete", "OK");
            }
            async void OnAddButtonClicked(object sender, EventArgs e)
            {
                var shopl = (ShopList)BindingContext;
                var selectedProduct = listView.SelectedItem as Product;

                if (selectedProduct != null)
                {
                    var listProduct = new ListProduct
                    {
                        ShopListID = shopl.ID,
                        ProductID = selectedProduct.ID
                    };

                    // Save the ListProduct relationship
                    await App.Database.SaveListProductAsync(listProduct);

                    // Update the list view with the new product
                    listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
                }
                else
                {
                    await DisplayAlert("No Selection", "Please select a product to add", "OK");
                }
            }
        }

    }
}