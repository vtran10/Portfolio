app is to pull 1 of 151 random pokemon and popluated data into a card

data comes from 3 source:
pokemon_data - contains data about a single pokemon
pokemon_species_data - contains data about a single pokemon and links with the evolution data
pokemon_chain_data - contains the evolution data

with these three sources of data pokemon card will show no evolution if pokemon is stage 1 
if stage 2 will show stage 1
if stage 3 will show stage 2

1. need to correctly format pokemon_type_symbols as it overflows the right side of card
2. code the info bar under main image
3. create 1st for stats, create 2nd tab for moves
4. code display stats or moves tabs
5. code 2nd and 3rd pokemon card 
  if pokemon has 1 evolution display 2 cards 
  if pokemon has 2 evolution display 3 cards
6. code arrow chaining evolution
7. change gradient and font color based on type

for testing pokemon 0 = mewtwo


aplineIndex
x-data="p: null" // must be assigned with the this.p for it to work
response = data > this.p = data
to console.log("p:" this.p) // will return proxy object
must do a seperate p = data to get data to disply in console
