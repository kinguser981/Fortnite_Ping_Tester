using DnsClient;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;



namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        private readonly ObservableCollection<string> dnsResults = new ObservableCollection<string>();
        
        public string PingRegion { get; set; }


        public MainPage()
        {
            InitializeComponent();
            
            Application.Current.UserAppTheme = AppTheme.Dark;

            // Set the ListView's ItemsSource property once in the constructor.
            // All future additions to the dnsResults collection will update the ListView.
            MyListView.ItemsSource = dnsResults;



            var RegionList = new List<string>
        {
            "NA-East:",
            "NA - Central:",
            "NA - West:",
            "Europe:",
            "Oceania:",
            "Brazil:",
            "Asia:",
            "Middle East:"

        };

            // 2. Assign the list to the Picker's ItemsSource
            
            RegionPicker.ItemsSource = RegionList;
            RegionPicker.SelectedIndexChanged += OnRegionPickerSelectedIndexChanged;    

        }
        private void OnRegionPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                string selectedRegion = (string)picker.SelectedItem;
                SelectedItemLabel.Text = $"Selected Region: {selectedRegion}";
                
                switch (selectedIndex)
                {
                    case 0:
                        PingRegion = "ping-nae.ds.on.epicgames.com";
                        break;
                    case 1:
                        PingRegion = "ping-nac.ds.on.epicgames.com";
                        break;
                    case 2:
                        PingRegion = "ping-naw.ds.on.epicgames.com";
                        break;
                    case 3:
                        PingRegion = "ping-eu.ds.on.epicgames.com";
                        break;
                    case 4:
                        PingRegion = "ping-oce.ds.on.epicgames.com";
                        break;
                    case 5:
                        PingRegion = "ping-br.ds.on.epicgames.com";
                        break;
                    case 6:
                        PingRegion = "ping-asia.ds.on.epicgames.com";
                        break;
                    case 7:
                        PingRegion = "ping-me.ds.on.epicgames.com";
                        break;
                    default:
                        PingRegion = " ";
                        break;
                }
            }
           
        }

        public async void PerformDnsTest()
        {
            // Clear any previous results
            dnsResults.Clear();



            string hostnameToTest = PingRegion;

            //string hostnameToTest = "ping-me.ds.on.epicgames.com";


            var dnsServers = new Dictionary<string, IPAddress>
            {
                { "Google DNS", IPAddress.Parse("8.8.8.8") },
                { "Cloudflare DNS", IPAddress.Parse("1.1.1.1") },
                { "OpenDNS", IPAddress.Parse("208.67.222.222") },
                { "Quad9", IPAddress.Parse("9.9.9.9") },
                { "Shecan DNS", IPAddress.Parse("178.22.122.100") },
                { "ElectoNet DNS", IPAddress.Parse("10.202.10.10") }
            };

            foreach (var server in dnsServers)
            {
                try
                {
                    var lookupClient = new LookupClient(server.Value);
                    var result = await lookupClient.QueryAsync(hostnameToTest, QueryType.A);
                    var ipAddress = result.Answers.OfType<DnsClient.Protocol.ARecord>().FirstOrDefault()?.Address;

                    if (ipAddress != null)
                    {
                        using (var ping = new Ping())
                        {
                            var reply = await ping.SendPingAsync(ipAddress, 4000);

                            string resultString;
                            if (reply.Status == IPStatus.Success)
                            {
                                resultString = $"{server.Key} ({server.Value}) - Ping time = {reply.RoundtripTime}ms";
                                
                            }
                            else
                            {
                                resultString = $"{server.Key} ({server.Value}) - Ping Failed: {reply.Status}";
                            }

                            // Add the result to the ObservableCollection.
                            // The ListView will update automatically.
                            dnsResults.Add(resultString);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    // Add an error message to the list
                    dnsResults.Add($"{server.Key} - Error: {ex.Message}");
                }
            }
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            // This button click can now trigger your DNS test.
            PerformDnsTest();
            SelectedItemLabel.Text = "Start to testing...";
            Task.Delay(4000).ContinueWith(t =>
            {
                SelectedItemLabel.Text = " Done!";
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            // DisplayAlert("DNS","has been Copy to the clipboard.  ","ok");
            if (e.SelectedItem == null)
            {
                return;
            }

            // Get the selected item, which is a string in your case
            string selectedItemText = (string)e.SelectedItem;

            // Use the Clipboard to copy the text.
             Clipboard.Default.SetTextAsync(selectedItemText);

            // Display a confirmation alert.
            DisplayAlert("Copied", "The DNS has been copied to the clipboard.", "OK");

            // Deselect the item to prevent the event from firing again when you navigate back.
            ((ListView)sender).SelectedItem = null;

        }


 


        private void MyListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

        }
    }
}