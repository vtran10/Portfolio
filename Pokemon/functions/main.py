from functions import get_pokemon_data, save_pokemon_data, print_pokemon

# set pokemon id number
pokemon_ids = range(1, 151 + 1)

for pokemon_id in pokemon_ids:
    # call the get_pokemon_data function
    response = get_pokemon_data(pokemon_id)

    # save_pokemon_data(content_type)
    save_pokemon_data(pokemon_id, response)

    # call the print_pokemon function
    print_pokemon(pokemon_id)