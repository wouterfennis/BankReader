﻿using System.Collections.Generic;
using System.Linq;

namespace Bankreader.Application.Models
{
    public class HouseholdBook
    {
        private readonly List<HouseholdPost> householdPosts;
        public HouseholdBook()
        {
            householdPosts = new List<HouseholdPost>();
        }

        public IReadOnlyList<HouseholdPost> HouseholdPosts { get => householdPosts; }

        public HouseholdPost RetrieveHouseholdPost(Category category)
        {
            var householdPost = householdPosts.SingleOrDefault(x => x.Category == category);
            if (householdPost == null)
            {
                householdPost = new HouseholdPost(category);
                householdPosts.Add(householdPost);
            }
            return householdPost;
        }
    }
}
