using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GwentDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GwentContext context = new GwentContext();
        public List<Card> Cards => context.Cards.ToList();
        public List<Collection> Collections => context.Collections.ToList();
        public List<Ability> Abilities => context.Abilities.ToList();

        public MainWindow()
        {
            InitializeComponent();
            comboBoxAbilities.ItemsSource = Abilities;
            dataGridCards.ItemsSource = Cards;
        }

        private void ButtonAddCard_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(textBoxStrength.Text, out int strength))
                MessageBox.Show("Strength needs to be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if(comboBoxAbilities.SelectedItem != null)
            {
                Ability ability = (Ability)comboBoxAbilities.SelectedItem;
                Card card = new Card(textBoxFaction.Text, textBoxName.Text, textBoxPosition.Text, strength, ability, textBoxType.Text);
                context.Cards.Add(card);
                context.SaveChanges();
                dataGridCards.Items.Refresh();
            }
        }

        private void ButtonRemoveCard_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridCards.SelectedItem != null)
            {
                context.Cards.Remove((Card)dataGridCards.SelectedItem);
                context.SaveChanges();
            }
        }

        private void ButtonAddAbility_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxAbility.Text))
            {
                Ability ability = new Ability(textBoxAbility.Text);
                context.Abilities.Add(ability);
                context.SaveChanges();
                comboBoxAbilities.Items.Refresh();
            }
        }

        private void ButtonRemoveAbility_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxAbilities.SelectedItem != null)
            {
                context.Abilities.Remove((Ability)comboBoxAbilities.SelectedItem);
                context.SaveChanges();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to clear the database?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                context.Cards.RemoveRange(context.Cards);
                context.Collections.RemoveRange(context.Collections);
                context.Abilities.RemoveRange(context.Abilities);
                context.SaveChanges();
            }
        }
    }
}
