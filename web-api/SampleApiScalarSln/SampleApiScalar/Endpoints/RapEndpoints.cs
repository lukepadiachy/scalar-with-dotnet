namespace SampleApiScalar.Endpoints
{
    public static class RapEndpoints
    {
        // In a real application, this data would come from a database.
        private static List<RapSong> _songs = new List<RapSong>
        {
            new RapSong { Id = 1, Title = "Flowin' Shadows", Artist = "Night Rhyme", Year = 2023 },
            new RapSong { Id = 2, Title = "Mystic Beats", Artist = "Dream Cipher", Year = 2021 },
            new RapSong { Id = 3, Title = "Urban Legends", Artist = "Rhythm Rebel", Year = 2022 }
        };

        public static void AddRapEndpoints(this WebApplication app)
        {
            // GET /rap - List all fictional rap songs
            app.MapGet("/rap", () =>
            {
                return Results.Ok(_songs);
            })
            .WithName("GetRapSongs")
            .WithTags("Rap");

            // GET /rap/{id} - Retrieve a rap song by its ID
            app.MapGet("/rap/{id:int}", (int id) =>
            {
                var song = _songs.FirstOrDefault(s => s.Id == id);
                return song is not null ? Results.Ok(song) : Results.NotFound();
            })
            .WithName("GetRapSongById")
            .WithTags("Rap");

            // POST /rap - Add a new fictional rap song
            app.MapPost("/rap", (RapSong song) =>
            {
                // Generate a new ID (max current ID + 1)
                song.Id = _songs.Any() ? _songs.Max(s => s.Id) + 1 : 1;
                _songs.Add(song);
                return Results.Created($"/rap/{song.Id}", song);
            })
            .WithName("CreateRapSong")
            .WithTags("Rap");

            // PUT /rap/{id} - Update an existing rap song
            app.MapPut("/rap/{id:int}", (int id, RapSong updatedSong) =>
            {
                var song = _songs.FirstOrDefault(s => s.Id == id);
                if (song is null)
                {
                    return Results.NotFound();
                }
                // Update the song's properties
                song.Title = updatedSong.Title;
                song.Artist = updatedSong.Artist;
                song.Year = updatedSong.Year;
                return Results.Ok(song);
            })
            .WithName("UpdateRapSong")
            .WithTags("Rap");

            // DELETE /rap/{id} - Remove a fictional rap song
            app.MapDelete("/rap/{id:int}", (int id) =>
            {
                var song = _songs.FirstOrDefault(s => s.Id == id);
                if (song is null)
                {
                    return Results.NotFound();
                }
                _songs.Remove(song);
                return Results.NoContent();
            })
            .WithName("DeleteRapSong")
            .WithTags("Rap");

            // GET /rap/random - Retrieve a random rap song
            app.MapGet("/rap/random", () =>
            {
                if (!_songs.Any())
                {
                    return Results.NotFound();
                }
                var random = new Random();
                var song = _songs[random.Next(_songs.Count)];
                return Results.Ok(song);
            })
            .WithName("GetRandomRapSong")
            .WithTags("Rap");

            // GET /rap/search?query=... - Search for rap songs by title or artist
            app.MapGet("/rap/search", (string query) =>
            {
                var results = _songs.Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                                s.Artist.Contains(query, StringComparison.OrdinalIgnoreCase));
                return Results.Ok(results);
            })
            .WithName("SearchRapSongs")
            .WithTags("Rap");
        }
    }

    // Record type for a rap song
    public record RapSong
    {
        public int Id { get; set; }
        public string Title { get; set; }  // Using set for modifiability during updates.
        public string Artist { get; set; }
        public int Year { get; set; }
    }
}
