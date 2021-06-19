﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorDemo.Pages
{
    public partial class BooksPage : ComponentBase, IAsyncDisposable
    {
        record Book(
            string Author,
            string Title,
            string RowKey = default,
            DateTimeOffset Timestamp = default);

        [Inject] HttpClient _httpClient { get; set; }

        HubConnection hubConnection;

        List<Book> books;
        string author;
        string title;
        bool isConnected;
        string echo;

        protected override async Task OnInitializedAsync()
        {
            echo = "Loading...";
            await ServiceConnect();
            await GetStored();
        }

        async Task ServiceConnect()
        {
            var url = "http://localhost:7071/api";
            try {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(url)
                    .Build();
                await hubConnection.StartAsync();
                isConnected = hubConnection.State == HubConnectionState.Connected;

                hubConnection.On<Book>("placedBook", (book) => {
                    books.Add(book);
                    echo = $"Count: {books.Count}";
                    StateHasChanged();
                });

                echo = "Connected.";
            }
            catch (Exception e) {
                echo = $"Exception: {e.Message}";
            }
        }

        async Task GetStored()
        {
            var url = "api/StoredOrders";
            try {
                books = await _httpClient.GetFromJsonAsync<List<Book>>(url);
                echo = $"Count: {books.Count}";
                // 
                if (books.Any() == false) {
                    author = "John Lennon";
                    title = "In His On Right";
                }
            }
            catch (Exception e) {
                echo = $"Exception: {e.Message}";
            }
        }

        async Task Send()
        {
            echo = "Sending...";
            var bookPlacement = new Book(author, title);
            try {
                var response = await _httpClient.PostAsJsonAsync("api/PlaceBook", bookPlacement);
            }
            catch (Exception e) {
                echo = $"Exception: {e.Message}";
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (isConnected) {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
