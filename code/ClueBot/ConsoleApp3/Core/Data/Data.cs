using ClueBot.Resources.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ClueBot.Core.Data
{
    public static class Data
    {
        public static int GetExp(ulong UserId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.ExperiencePoints.Where(x => x.UserId == UserId).Count() < 1)
                    return 0;
                return DbContext.ExperiencePoints.Where(x => x.UserId == UserId).Select(x => x.Amount).FirstOrDefault();
            }
        }
    

        public static async Task SaveExp(ulong UserId, int Amount)
        {
            using (var DbContext = new SqliteDbContext())
            {

                if (DbContext.ExperiencePoints.Where(x => x.UserId == UserId).Count() < 1)
                {
                    //the user has no row yet, so create one.
                    DbContext.ExperiencePoints.Add(new Resources.Database.Experience
                    {
                        UserId = UserId,
                        Amount = Amount
                    });
                }
                else
                {
                    Resources.Database.Experience Current = DbContext.ExperiencePoints.Where(x => x.UserId == UserId).FirstOrDefault();
                    Current.Amount += Amount;
                    DbContext.ExperiencePoints.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
