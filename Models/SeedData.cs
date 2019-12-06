namespace YourSpace.Models
{
    using global::YourSpace.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    namespace YourSpace.Models
    {
        public static class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new PhotoContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<PhotoContext>>()))
                {
                    // Look for any movies.
                    if (context.Photo.Any())
                    {
                        return;   // DB has been seeded
                    }

                    context.Photo.AddRange(
                        new Photos
                        {
                            Title = "Dog",
                            Description = "This is a photo of a dog.",
                            URL = "https://images.unsplash.com/photo-1518020382113-a7e8fc38eac9?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=360&q=80"

                        },

                        new Photos
                        {
                            Title = "Cat",
                            Description = "This is a photo of a cat.",
                            URL = "https://images.unsplash.com/photo-1548681528-6a5c45b66b42?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=334&q=80"

                        },

                        new Photos
                        {
                            Title = "Coffee",
                            Description = "This is a photo of cup of coffee.",
                            URL = "https://images.unsplash.com/photo-1489533119213-66a5cd877091?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=751&q=80"

                        },

                        new Photos
                        {
                            Title = "Programming",
                            Description = "This is a photo of programming.",
                            URL = "https://images.unsplash.com/photo-1509966756634-9c23dd6e6815?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=410&q=80"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
