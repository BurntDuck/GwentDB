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
        readonly GwentContext context = new GwentContext();
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
                    try
                    {
                        Card card = new Card((Factions)comboBoxFactions.SelectedItem, textBoxName.Text, (Positions)comboBoxPositions.SelectedItem, strength, (Ability)comboBoxAbilities.SelectedItem, (Types)comboBoxTypes.SelectedItem, (bool)checkBoxInCollection.IsChecked);
                        context.Cards.Add(card);
                        context.SaveChanges();

                        dataGridCards.ItemsSource = null;
                        dataGridCards.ItemsSource = Cards;
                    }
                    catch(ArgumentException)
                    {
                        MessageBox.Show("Error while creating new card", "\"Type\" and \"Position\" must both have value \"Leader\" or none of them can");
                    }
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

        /// <summary>
        /// Update the selected card in the database.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
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
                        try
                        {
                            Card card = context.Cards.Find(selectedCard.Id);
                            card.Update((Factions)comboBoxFactions.SelectedItem, textBoxName.Text, (Positions)comboBoxPositions.SelectedItem, strength, (Ability)comboBoxAbilities.SelectedItem, (Types)comboBoxTypes.SelectedItem, (bool)checkBoxInCollection.IsChecked);
                            context.SaveChanges();

                            dataGridCards.ItemsSource = null;
                            dataGridCards.ItemsSource = Cards;
                            dataGridCards.SelectedItem = card;
                            dataGridCards.ScrollIntoView(card);
                        }
                        catch(ArgumentException)
                        {
                            MessageBox.Show("Error while updating card", "\"Type\" and \"Position\" must both have value \"Leader\" or none of them can");
                        }
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

        /// <summary>
        /// Update the selected ability in the database.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
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

        /// <summary>
        /// Fill textBoxAbility with the selected ability.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
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

                comboBoxAbilities.ItemsSource = null;
                comboBoxAbilities.ItemsSource = Abilities;
                dataGridCards.ItemsSource = null;
                dataGridCards.ItemsSource = Cards;
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

        /// <summary>
        /// Populates the database with sample data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event itself.</param>
        private void ButtonAutoPoulate_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you want to populate the database with sample data?\nThis will delete all previous entries permanently!", "Auto populate", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Ability> abilities = new List<Ability>()
                {
                new Ability("Agile"),
                new Ability("Bond"),
                new Ability("Leader"),
                new Ability("Medic"),
                new Ability("Morale"),
                new Ability("Muster"),
                new Ability("Scorch"),
                new Ability("Spy")
                };
                context.Abilities.RemoveRange(context.Abilities);
                context.Abilities.AddRange(abilities);

                List<Card> cards = new List<Card>()
                {
                new Card(Factions.Neutral, "Geralt of Rivia", Positions.Melee, 15, Types.Hero, true),
                new Card(Factions.Neutral, "Cirilla Fiona Elen Raianno", Positions.Melee, 15, Types.Hero, true),
                new Card(Factions.Neutral, "Vesemir", Positions.Melee, 15, Types.Normal, true),
                new Card(Factions.Neutral, "Yennefer of Vengerberg", Positions.Archer, 15, abilities[3], Types.Hero, false),
                new Card(Factions.Neutral, "Triss Merigold", Positions.Melee, 7, Types.Hero, false),
                new Card(Factions.Neutral, "Dandelion", Positions.Melee, 2, abilities[1], Types.Normal, false),
                new Card(Factions.Neutral, "Zoltan Chivay", Positions.Melee, 5, Types.Normal, false),
                new Card(Factions.Neutral, "Emiel Regis Rohellec Terzieff", Positions.Melee, 5, Types.Normal, false),
                new Card(Factions.Neutral, "Villentretenmerth", Positions.Melee, 7, abilities[6], Types.Normal, false),
                new Card(Factions.Neutral, "Avallac'h", Positions.Melee, 0, abilities[7], Types.Hero, false),
                new Card(Factions.Neutral, "Decoy", Positions.Any, 0, Types.Normal, true),
                new Card(Factions.Neutral, "Commander's Horn", Positions.Any, 0, Types.Normal, false),
                new Card(Factions.Neutral, "Scorch", Positions.Any, 0, Types.Normal, false),
                new Card(Factions.Neutral, "Biting Frost", Positions.Melee, 0, Types.Normal, false),
                new Card(Factions.Neutral, "Impenetrable Fog", Positions.Archer, 0, Types.Normal, false),
                new Card(Factions.Neutral, "Torrential Rain", Positions.Siege, 0, Types.Normal, false),
                new Card(Factions.Neutral, "Clear Weather", Positions.Any, 0, Types.Normal, true),

                new Card(Factions.NorthernRealms, "The Steel Forged", Positions.Leader, 0, abilities[3], Types.Leader, true),
                new Card(Factions.NorthernRealms, "The Siegemaster", Positions.Leader, 0, abilities[3], Types.Leader, true),
                new Card(Factions.NorthernRealms, "Lord Commander of the North", Positions.Leader, 0, abilities[3], Types.Leader, false),
                new Card(Factions.NorthernRealms, "King of Temeria", Positions.Leader, 0, abilities[3], Types.Leader, true),

                new Card(Factions.NorthernRealms, "Vernon Roche", Positions.Melee, 10, Types.Hero, true),
                new Card(Factions.NorthernRealms, "John Natalis", Positions.Melee, 10, Types.Hero, false),
                new Card(Factions.NorthernRealms, "Esterad Thyssen", Positions.Melee, 10, Types.Hero, false),
                new Card(Factions.NorthernRealms, "Philippa Eilhart", Positions.Archer, 10, Types.Hero, true),
                new Card(Factions.NorthernRealms, "Thaler", Positions.Siege, 1, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Ves", Positions.Melee, 5, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Siegfried of Denesle", Positions.Melee, 5, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Yarpen Zigrin", Positions.Melee, 2, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Sigismund Dijkstra", Positions.Melee, 4, abilities[7], Types.Normal, false),
                new Card(Factions.NorthernRealms, "Keira Metz", Positions.Archer, 5, Types.Hero, false),
                new Card(Factions.NorthernRealms, "Síle de Tansarville", Positions.Archer, 5, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Sabrina Glevissig", Positions.Archer, 4, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Sheldon Skaggs", Positions.Archer, 4, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Dethmold", Positions.Archer, 6, Types.Normal, true),
                new Card(Factions.NorthernRealms, "Prince Stennis", Positions.Melee, 5, abilities[7], Types.Normal, true),
                new Card(Factions.NorthernRealms, "Trebuchet", Positions.Siege, 6, Types.Normal, true),
                new Card(Factions.NorthernRealms, "Crinfrid Reavers Dragon Hunter", Positions.Archer, 5, abilities[1], Types.Normal, false),
                new Card(Factions.NorthernRealms, "Redanian Foot Soldier", Positions.Melee, 1, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Catapult", Positions.Siege, 8, abilities[1], Types.Normal, false),
                new Card(Factions.NorthernRealms, "Ballista", Positions.Siege, 6, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Kaedweni Siege Expert", Positions.Siege, 1, abilities[5], Types.Normal, false),
                new Card(Factions.NorthernRealms, "Blue Stripes Commando", Positions.Melee, 4, abilities[1], Types.Normal, false),
                new Card(Factions.NorthernRealms, "Siege Tower", Positions.Siege, 6, Types.Normal, false),
                new Card(Factions.NorthernRealms, "Dun Banner Medic", Positions.Siege, 5, abilities[4], Types.Normal, false),

                new Card(Factions.Niflgaard, "The Relentless", Positions.Leader, 0, abilities[3], Types.Leader, true),
                new Card(Factions.Niflgaard, "The White Flame", Positions.Leader, 0, abilities[3], Types.Leader, false),
                new Card(Factions.Niflgaard, "The Emperor of Niflgaard", Positions.Leader, 0, abilities[3], Types.Leader, false),
                new Card(Factions.Niflgaard, "His Imperial Majesty", Positions.Leader, 0, abilities[3], Types.Leader, true),

                new Card(Factions.Niflgaard, "Letho of Gulet", Positions.Melee, 10, Types.Hero, true),
                new Card(Factions.Niflgaard, "Menno Coehoorn", Positions.Melee, 10, Types.Hero, false),
                new Card(Factions.Niflgaard, "Morvran Voorhis", Positions.Siege, 10, Types.Hero, false),
                new Card(Factions.Niflgaard, "Tibor Eggebracht", Positions.Archer, 10, Types.Hero, true),
                new Card(Factions.Niflgaard, "Albrich", Positions.Archer, 2, Types.Normal, false),
                new Card(Factions.Niflgaard, "Assire var Anahid", Positions.Archer, 6, Types.Normal, false),
                new Card(Factions.Niflgaard, "Cynthia", Positions.Archer, 4, Types.Normal, true),
                new Card(Factions.Niflgaard, "Fringilla Vigo", Positions.Archer, 6, Types.Normal, false),
                new Card(Factions.Niflgaard, "Morteisen", Positions.Melee, 3, Types.Normal, false),
                new Card(Factions.Niflgaard, "Rainfarn", Positions.Melee, 4, Types.Normal, false),
                new Card(Factions.Niflgaard, "Renuald aep Matsen", Positions.Archer, 5, Types.Normal, false),
                new Card(Factions.Niflgaard, "Rotten Mangonel", Positions.Siege, 3, Types.Normal, false),
                new Card(Factions.Niflgaard, "Shilard Fitz-Oesterlen", Positions.Melee, 7, abilities[7], Types.Normal, false),
                new Card(Factions.Niflgaard, "Stefan Skellen", Positions.Melee, 9, abilities[7], Types.Normal, false),
                new Card(Factions.Niflgaard, "Sweers", Positions.Archer, 2, Types.Normal, false),
                new Card(Factions.Niflgaard, "Vanhemar", Positions.Archer, 4, Types.Normal, false),
                new Card(Factions.Niflgaard, "Vattier de Rideaux", Positions.Melee, 4, abilities[7], Types.Normal, false),
                new Card(Factions.Niflgaard, "Vreemde", Positions.Melee, 2, Types.Normal, false),
                new Card(Factions.Niflgaard, "Cahir Mawr Dyffryn aep Ceallach", Positions.Melee, 6, Types.Normal, false),
                new Card(Factions.Niflgaard, "Puttkammer", Positions.Archer, 3, Types.Normal, true),
                new Card(Factions.Niflgaard, "Etolian Auxiliary Archers", Positions.Archer, 1, abilities[4], Types.Normal, false),
                new Card(Factions.Niflgaard, "Black Infantry Archer", Positions.Archer, 1, Types.Normal, false),
                new Card(Factions.Niflgaard, "Siege Technican", Positions.Siege, 0, abilities[4], Types.Normal, true),
                new Card(Factions.Niflgaard, "Heavy Zerrikanian Fire Scorpion", Positions.Siege, 10, Types.Normal, false),
                new Card(Factions.Niflgaard, "Zerrikanian Fire Scorpion", Positions.Siege, 10, Types.Normal, true),
                new Card(Factions.Niflgaard, "Impera Brigade", Positions.Melee, 3, abilities[1], Types.Normal, false),
                new Card(Factions.Niflgaard, "Nausicaa Cavalry Brigade", Positions.Melee, 2, abilities[1], Types.Normal, false),
                new Card(Factions.Niflgaard, "Siege Engineer", Positions.Siege, 6, Types.Normal, false),
                new Card(Factions.Niflgaard, "Young Emissary", Positions.Melee, 5, abilities[1], Types.Normal, false)
                };

                context.Cards.RemoveRange(context.Cards);
                context.Cards.AddRange(cards);
                context.SaveChanges();

                comboBoxAbilities.ItemsSource = null;
                comboBoxAbilities.ItemsSource = Abilities;
                dataGridCards.ItemsSource = null;
                dataGridCards.ItemsSource = Cards;
            }
        }
    }
}
