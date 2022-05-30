using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_LumbanKuizon.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();

            global::System.Object p = CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener((snapshot, error) =>
                {
                    //loading.IsVisible = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ContactModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(item);
                                    }
                                    break;
                            }
                        }
                    }
                    emptyListLabel.IsVisible = contactList.Count == 0;
                    contactsList.IsVisible = !(contactList.Count == 0);
                    //loading.IsVisible = false;
                });
        }

        public async void searchFunction(object sender, EventArgs e)
        {
            var documents = await CrossCloudFirestore.Current
                .Instance
                .GetCollection("users")
                .WhereEqualsTo("email", param)
                .GetDocumentsAsyn();
            foreach (var documentChange in documents.DocumentChanges)
            {
                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = JsonConvert.DeserializeObject<UserModel>(json);
                result.Add(obj);
            }
            resultList.ItemsSource = result;

            if(result.Count == 0)
            {
                await DisplayAlert("", "User not found", "Okay");
                await Navigation.PopModalAsync(true);
            }
        }

        public async void addContact(object sender, EventArgs e)
        {
            ContactModel contact = new ContactModel()
            {
                id = IDGenerator.generateID(),
                contactID = new string[] { DataClass.GetInstance.loggedInUser.uid, item.uid},
                contactEmail = new string[] { DataClass.GetInstance.loggedInUser.email, item.email },
                contactName = new string[] { DataClass.GetInstance.loggedInUser.name, item.name },
                created_at = DateTime.UtcNow
            };
            //contacts
            await CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(contact.id)
                .SetDataAsync(contact);
            //users(owner) => contacts
            if (dataClass.loggedInUser.contacts == null)
                dataClass.loggedInUser.contacts = new List<string>();
            dataClass.loggedInUser.contacts.Add(item.uid);
            await CrossCloudFirestore.Current
                .Instance
                .GetCollection("users")
                .GetDocument(dataClass.loggedInUser.uid)
                .UpdateDataAsync(new {contacts = dataClass.loggedInUser.contacts });
            //users(addedContact) -> contacts
            if (item.contacts == null)
                item.contacts = new List<string>();
            item.contacts.Add(dataClass.loggedInUser.uid);
            await CrossCloudFirestore.Current
                .instance
                .GetCollection("users")
                .GetDocument(item.uid)
                .UpdateDataAsync(new { contacts = item.contacts });

            await DisplayAlert("Success", "Contact Added!", "Okay");
        }

        public async void getConversations(object sender, EventArgs e)
        {
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(contact.id)
                .GetCollection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    conversationListView.ItemsSource = conversationList;
                    if(snapshot != null)
                    {
                        foreach(var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConversationModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c=> c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }
                            var conv = conversationListView.ItemsSource.Cast<object>().LastOrDefault();
                            conversationListView.ScrollTo(conv, ScrollToPosition.End, false);
                        }
                    }
                    emptyListLabel.isVisible = conversationList.Count == 0;
                    conversationListView.IsVisible = !(conversationList.Count == 0);
                });
        }

        public async void addConversation(object sender, EventArgs e)
        {
            string ID = IDGenerator.generateID();
            ConversationModel conversation = new ConversationModel()
            {
                id = ID,
                converseID = dataClass.loggedInUser.uid,
                message = editor.Text,
                created_at = DateTime.UtcNow
            };

            //conversation
            await CrossCloudFirestore.Current
                .Instance
                .GetCollection("contacts")
                .GetDocument(contact.id)
                .GetCollection("conversations")
                .GetDocument(ID)
                .SetDataAsync(conversation);
            editor.Text = string.Empty;
        }
    }
}