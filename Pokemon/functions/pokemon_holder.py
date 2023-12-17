
# Create a list of dictionaries that represent different pokemon.
pokemons = [
    {
        "id": 1,
        "name": "papalapa",
        "height": 9,
        "weight": 99,
        "sprite_url": "https://i.etsystatic.com/40903084/r/il/c723fa/5459256121/il_794xN.5459256121_peqb.jpg",
        "types": ["grass", "poison"],
        "stats": [
            {"stat": "hp", "base_value": 11},
            {"stat": "attack", "base_value": 22},
            {"stat": "defense", "base_value": 33},
            {"stat": "special-attack", "base_value": 44},
            {"stat": "special-defense", "base_value": 55},
            {"stat": "speed", "base_value": 66},
        ],
        "moves": [
            {"move": "papa-slap", "effect": "physical attack"},
            {"move": "papa-doodoo", "effect": "gas attack, disorients enemy"},
            {"move": "papa-roar", "effect": "buffs physical attack x2"},
            {"move": "papa-special-attack", "effect": "flurry of kisses and hugs"},
        ]
    },
]
def print_pokemon(pokemons):
    for pokemon in pokemons:
        print(f"name: {pokemon['name']}")
        print(f"height: {pokemon['height']}")
        print(f"weight: {pokemon['weight']}")

        types = pokemon["types"]
        types_String = []
        
        for type in types:
            types_String.append(type)        
        result = pokemon["types"]
        print("Types:", ", ".join(result))

        i = 0
        stats = pokemons[0]["stats"]

        for stat in stats:
            statName = pokemons[0]["stats"][i]["stat"]
            baseValue = pokemons[0]["stats"][i]["base_value"]
            print(f"{statName}: {baseValue}")
            i += 1

        j = 0
        moves = pokemons[0]["moves"]
        for move in moves:
            moveName = pokemons[0]["moves"][j]["move"]
            effect = pokemons[0]["moves"][j]["effect"]
            print(f"move {j}: " + moveName + " - " + effect)
            j += 1

print_pokemon(pokemons)