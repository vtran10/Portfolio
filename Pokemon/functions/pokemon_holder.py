
# pokemons = [
#     {
#         "name": "papalapa",
#         "height": 9,
#         "weight": 99,
#         "sprite_url": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png", 
#         "stats": ["stats"],
#         "type": ["type"]
#     }
# ]

pokemons = [
    {
        "name": "papalapa",
        "height": 9,
        "weight": 99,
        "types": ["grass", "poison"],
        "stats": [
            {"stat": "hp", "base_value": 11},
            {"stat": "attack", "base_value": 22},
            {"stat": "defense", "base_value": 33},
            {"stat": "special-attack", "base_value": 44},
            {"stat": "special-defense", "base_value": 55},
            {"stat": "speed", "base_value": 66},
        ]

    },    
]


#how to access hp and value
# print(pokemons[0]["stats"][0]["stat"])
# print(pokemons[0]["stats"][0]["base_value"])

# new_pokemon = {
#     "name": "kailanlan",
#     "height": 7,
#     "weight": 7,
     
# }

# pokemons.append(new_pokemon)

# print(pokemons)

for pokemon in pokemons:
    # name = pokemon["name"]
    # print(f"name: {name}")
    print(f"name: {pokemon['name']}")
    print(f"height: {pokemon['height']}")
    print(f"weight: {pokemon['weight']}")

    types = pokemon["types"]
    i=1
    for type in types:
        print(f"type {i}: " + type, end=" ")
        i+=1

    print("\n")

    # #how to access dictionary hp and value data
    # print(pokemons[0]["stats"][0]["stat"])
    # print(pokemons[0]["stats"][0]["base_value"])

    i=0
    stats = pokemons[0]["stats"]

    for stat in stats:
        statName = pokemons[0]["stats"][i]["stat"]
        baseValue = pokemons[0]["stats"][i]["base_value"]
        print(f"{statName}: {baseValue}")
        i+=1