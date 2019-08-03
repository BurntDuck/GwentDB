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
        List<Card> Cards => context.Cards.ToList();
        List<Ability> Abilities => context.Abilities.ToList();
        Card selectedCard;

        public MainWindow()
        {
            InitializeComponent();
            comboBoxFactions.ItemsSource = Enum.GetValues(typeof(Factions)).Cast<Factions>();
            comboBoxPositions.ItemsSource = Enum.GetValues(typeof(Positions)).Cast<Positions>();
            comboBoxTypes.ItemsSource = Enum.GetValues(typeof(Types)).Cast<Types>();
            comboBoxAbilities.ItemsSource = Abilities;
            dataGridCards.ItemsSource = Cards;
        }

        /// <summary>
        /// Add a Gwent card to the database.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonAddCard_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxFactions.SelectedItem != null && !string.IsNullOrEmpty(textBoxName.Text) && comboBoxPositions.SelectedItem != null && !string.IsNullOrEmpty(textBoxStrength.Text) && comboBoxAbilities.SelectedItem != null && comboBoxTypes.SelectedItem != null)
            {
                if(!int.TryParse(textBoxStrength.Text, out int strength))
                    MessageBox.Show("Strength needs to be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(comboBoxAbilities.SelectedItem != null)
                {
                    Card card = new Card((Factions)comboBoxFactions.SelectedItem, textBoxName.Text, (Positions)comboBoxPositions.SelectedItem, strength, (Ability)comboBoxAbilities.SelectedItem, (Types)comboBoxTypes.SelectedItem, (bool)checkBoxInCollection.IsChecked);
                    context.Cards.Add(card);
                    context.SaveChanges();

                    dataGridCards.ItemsSource = null;
                    dataGridCards.ItemsSource = Cards;
                }
            }
            else
                MessageBox.Show("All fields needs to be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Remove a gwent card from the database.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonRemoveCard_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridCards.SelectedItem != null)
            {
                context.Cards.Remove((Card)dataGridCards.SelectedItem);
                context.SaveChanges();

                dataGridCards.ItemsSource = null;
                dataGridCards.ItemsSource = Cards;
            }
        }

        private void ButtonUpdateCard_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridCards.SelectedItem != null)
            {
                if(comboBoxFactions.SelectedItem != null && !string.IsNullOrEmpty(textBoxName.Text) && comboBoxPositions.SelectedItem != null && !string.IsNullOrEmpty(textBoxStrength.Text) && comboBoxAbilities.SelectedItem != null && comboBoxTypes.SelectedItem != null)
                {
                    if(!int.TryParse(textBoxStrength.Text, out int strength))
                        MessageBox.Show("Strength needs to be an integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if(comboBoxAbilities.SelectedItem != null)
                    {
                        Card card = context.Cards.Find(selectedCard.Id);
                        card.Update((Factions)comboBoxFactions.SelectedItem, textBoxName.Text, (Positions)comboBoxPositions.SelectedItem, strength, (Ability)comboBoxAbilities.SelectedItem, (Types)comboBoxTypes.SelectedItem, (bool)checkBoxInCollection.IsChecked);
                        context.SaveChanges();

                        dataGridCards.ItemsSource = null;
                        dataGridCards.ItemsSource = Cards;
                    }
                }
                else
                    MessageBox.Show("All fields needs to be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Add a card abaility to the database.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonAddAbility_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxAbility.Text))
            {
                Ability ability = new Ability(textBoxAbility.Text);
                context.Abilities.Add(ability);
                context.SaveChanges();

                comboBoxAbilities.ItemsSource = null;
                comboBoxAbilities.ItemsSource = Abilities;
                comboBoxAbilities.SelectedItem = ability;
            }
        }

        /// <summary>
        /// Remove a card ability from the databse.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonRemoveAbility_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxAbilities.SelectedItem != null)
            {
                context.Abilities.Remove((Ability)comboBoxAbilities.SelectedItem);
                context.SaveChanges();

                comboBoxAbilities.ItemsSource = null;
                comboBoxAbilities.ItemsSource = Abilities;
            }
        }

        private void ButtonUpdateAbility_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxAbilities.SelectedItem != null)
            {
                Ability ability = (Ability)comboBoxAbilities.SelectedItem;
                context.Abilities.Find(ability.Id).Update(textBoxAbility.Text);
                context.SaveChanges();

                comboBoxAbilities.ItemsSource = null;
                comboBoxAbilities.ItemsSource = Abilities;
                comboBoxAbilities.SelectedItem = ability;
                dataGridCards.ItemsSource = null;
                dataGridCards.ItemsSource = Cards;
            }
        }

        private void ComboBoxAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxAbilities.SelectedItem != null)
            {
                Ability ability = (Ability)comboBoxAbilities.SelectedItem;
                textBoxAbility.Text = ability.Name;
            }
        }

        /// <summary>
        /// Clear the database of all data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure, you want to clear the database?\nThis action cannot be undone.", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                context.Cards.RemoveRange(context.Cards);
                context.Abilities.RemoveRange(context.Abilities);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Fill the text- and comboboxes with the data of the selected card.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void DataGridCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridCards.SelectedItem!= null)
            {
                selectedCard = (Card)dataGridCards.SelectedItem;
                comboBoxFactions.SelectedItem = selectedCard.Faction;
                textBoxName.Text = selectedCard.Name;
                comboBoxPositions.SelectedItem = selectedCard.Position;
                textBoxStrength.Text = selectedCard.Strength.ToString();
                comboBoxAbilities.SelectedItem = selectedCard.Ability;
                comboBoxTypes.SelectedItem = selectedCard.Type;
                checkBoxInCollection.IsChecked = selectedCard.InCollection;
            }
        }

        /// <summary>
        /// Ensures either both comboBoxPositions and comboBoxTypes have selected "Leader" or none of the have.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ComboBoxPositions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxPositions.SelectedItem != null && (Positions)comboBoxPositions.SelectedItem == Positions.Leader)
                comboBoxTypes.SelectedItem = Types.Leader;
            else if(comboBoxTypes.SelectedItem != null && (Types)comboBoxTypes.SelectedItem == Types.Leader)
                comboBoxTypes.SelectedItem = null;
        }


        /// <summary>
        /// Ensures either both comboBoxTypes and comboBoxPositions have selected "Leader" or none of the have.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ComboBoxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxTypes.SelectedItem != null && (Types)comboBoxTypes.SelectedItem == Types.Leader)
                comboBoxPositions.SelectedItem = Positions.Leader;
            else if(comboBoxPositions.SelectedItem != null && (Positions)comboBoxPositions.SelectedItem == Positions.Leader)
                comboBoxPositions.SelectedItem = null;
        }
    }
}
