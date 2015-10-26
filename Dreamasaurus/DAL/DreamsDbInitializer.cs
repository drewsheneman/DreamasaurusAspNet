using System.Collections.Generic;
using System.Linq;
using Dreamasaurus.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dreamasaurus.DAL
{
    public class DreamsDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DreamsDbContext>
    {
        protected override void Seed(DreamsDbContext context)
        {
            #region Create Admin User Role

            var roleManager = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Admin"));

            context.SaveChanges();

            #endregion

            #region Create an Admin User and Normal User

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!(context.Users.Any(u => u.UserName == "drew@test.com")))
            {
                var userToInsert = new ApplicationUser { UserName = "drew@test.com", PhoneNumber = "5555555" };
                userManager.Create(userToInsert, "Password1.");
            }
            context.SaveChanges();

            if (!(context.Users.Any(u => u.UserName == "test@abc.com")))
            {
                var userToInsert = new ApplicationUser { UserName = "test@abc.com", PhoneNumber = "5555555" };
                userManager.Create(userToInsert, "Password1.");
            }
            context.SaveChanges();

            var user = userManager.FindByName("drew@test.com");
            userManager.AddToRole(user.Id, "Admin");

            context.SaveChanges();

            #endregion

            #region Create Dreams for both users

            var dreams = new List<Dream>
            {
            new Dream{User=user, Title="Lorem ipsum dolor sit amet",Text="Suspendisse potenti. Nam commodo vel est in dictum. Pellentesque quis mattis quam. Pellentesque vitae leo dictum, tincidunt ligula ac, finibus urna. Donec elementum sapien et malesuada pharetra. Donec ultrices blandit venenatis. Ut semper diam quis libero volutpat efficitur. Pellentesque eu ante lectus. Suspendisse interdum dapibus commodo. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent consequat sit amet felis non tempor. Etiam efficitur in odio sed pellentesque. Nulla laoreet libero id metus porta tincidunt."},
            new Dream{User=user, Title="Aliquam tristique condimentum quam.",Text="Donec eros arcu, lacinia eget commodo sit amet, dignissim ac dui. Pellentesque aliquam suscipit porta. Proin porta arcu vitae lorem elementum, id ultrices turpis dignissim. In tristique velit in hendrerit porttitor. Vestibulum sit amet libero sodales, gravida dui nec, sollicitudin leo. Suspendisse sit amet augue semper diam auctor ullamcorper sed gravida mauris. Quisque hendrerit consequat turpis, non lobortis mauris mattis at. Maecenas egestas blandit nunc lacinia vehicula. Maecenas metus tortor, euismod sit amet interdum in, rutrum in arcu. Integer luctus metus sed velit iaculis, vel gravida purus sagittis. Donec scelerisque odio ac augue mattis, eget eleifend libero pellentesque."},
            new Dream{User=user, Title="Fusce eget gravida metus, sed mattis elit. ",Text="Ut a suscipit lacus. Curabitur non mi laoreet, lacinia magna nec, pretium enim. Morbi dui elit, pharetra ut orci vulputate, fringilla porta magna. Vivamus in eros tellus. Quisque elementum eros nec egestas eleifend. Donec condimentum id mi quis congue. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate diam id elit vulputate, in dignissim sapien porta. Suspendisse magna dui, elementum in sapien sit amet, tincidunt ullamcorper velit. Nulla suscipit felis in libero tincidunt egestas. Integer est dui, pretium luctus neque eleifend, congue tincidunt metus. Nam posuere fermentum sapien, eget auctor ligula mattis mattis. Morbi volutpat orci sit amet nunc aliquam, id pretium elit maximus. Proin facilisis efficitur pretium."},
            new Dream{User=user, Title="Nunc elementum elementum dapibus.",Text="Vivamus bibendum auctor orci, sed pulvinar massa finibus et. Maecenas eu laoreet mi. Phasellus ut metus elementum, cursus metus ut, ultricies sem. Donec ut ligula eu nisl porta mollis in vel dui. Donec aliquam, libero non aliquet gravida, justo lorem facilisis ligula, sed porta arcu velit sit amet sapien. Suspendisse id neque lacinia, blandit arcu ut, dictum dui. Nullam volutpat dui quis turpis pharetra fringilla sit amet at tortor. Nulla sed pretium erat. Suspendisse egestas ipsum eu lacus iaculis tempor."},
            new Dream{User=user, Title="In ac massa egestas, dignissim metus vitae, gravida neque.",Text="Mauris feugiat, orci et cursus interdum, elit augue consectetur enim, at imperdiet libero nibh vitae ligula. Curabitur sit amet ullamcorper purus, ac aliquam ligula. Praesent eu libero fermentum, mollis velit vel, eleifend metus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed vitae suscipit ipsum. Etiam sed metus a leo consectetur venenatis sit amet sed justo. In eget est malesuada, interdum leo non, dictum tortor. Donec ac ante convallis, hendrerit nisl ut, tristique erat. Phasellus rutrum lorem eget dolor accumsan cursus."}
            };

            dreams.ForEach(s => context.Dreams.Add(s));
            context.SaveChanges();

            var user2 = userManager.FindByName("test@abc.com");
            var dreams2 = new List<Dream>
            {
            new Dream{User=user2, Title="Rich organic blue mountain medium cream brewed",Text="milk foam ristretto grounds medium. Pumpkin spice a, qui et cappuccino redeye shop dripper irish. Skinny whipped lungo kopi-luwak to go as affogato. Grounds turkish body est bar and saucer cinnamon. Redeye, variety ristretto sugar fair trade mug french press."},
            new Dream{User=user2, Title="Kopi-luwak, single origin, robust breve sweet americano grinder",Text="Mocha, est blue mountain con panna, cup cultivar seasonal single origin redeye that. Est skinny mocha saucer est saucer acerbic irish. Iced carajillo coffee flavour, cup macchiato aromatic rich macchiato. Dark, single shot, bar that saucer cappuccino froth ut crema variety."},
            new Dream{User=user2, Title="Id extraction, doppio at foam crema half and half",Text="Con panna, that, cortado est beans a whipped instant. Sweet extra fair trade shop, percolator aftertaste arabica, crema percolator breve chicory galão. Turkish, wings acerbic as single origin crema white extraction. Chicory, grinder mug, galão, french press sugar irish plunger pot cup shop."},
            };

            dreams2.ForEach(s => context.Dreams.Add(s));
            context.SaveChanges();

            #endregion

        }
    }
}