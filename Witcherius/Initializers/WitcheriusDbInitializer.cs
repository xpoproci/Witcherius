using System.Data.Entity;

namespace Witcherius.Initializers
{
    public class WitcheriusDbInitializer : DropCreateDatabaseIfModelChanges<WitcheriusDbContext>
    {
        protected override void Seed(WitcheriusDbContext context)
        {
            base.Seed(context);
        }
    }
}
