using System;

namespace DietAndFitness.Views
{

    public class HomePageMenuItem
    {
        public HomePageMenuItem()
        {
            TargetType = typeof(DailyFoodListPage);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}