# import requests module
import requests

def get_pokemon_data(num):
    # set id of the pokemon you want to get data from
    id = num

    # set url to get data from
    url = f"https://pokeapi.co/api/v2/pokemon-species/{id}"

    # get data from url
    response = requests.get(url)

    # if response is successful print success message else fail message
    if response.status_code == 200:
        print("Request status code is 200 successful")
        return response
    else:
        print(f"Request failed with status code: {response.status_code}")

def save_pokemon_data(num, data):
    id = num
    response = data

    try:
        response.raise_for_status()  # Raise an HTTPError for bad responses

        # Get the content type from the response headers
        content_type = response.headers.get('Content-Type')

        # If the content is JSON, save it as a JSON file
        if content_type == 'application/json; charset=utf-8':
            file_path = f"../pokemon_sdata/pokemon_{id}_sdata.json"
            with open(file_path, "w", encoding="utf-8") as file:
                file.write(response.text)
            print(f"JSON data saved as '{file_path}'")
        else:
            # For other types of content, save it as a binary file
            file_path = f"../pokemon_sdata/pokemon_{id}_sdata.bin"
            with open(file_path, "wb") as file:
                file.write(response.content)
            print(f"Binary data saved as '{file_path}'")

    except requests.exceptions.HTTPError as http_err:
        print(f"HTTP error occurred: {http_err}")
    except Exception as err:
        print(f"An error occurred: {err}")


#import pokemon_data.json
import json

def print_pokemon(num):

    id = num

    file_path = rf"../pokemon_data/pokemon_{id}_sdata.json"

    with open(file_path, "r") as file:
        data = json.load(file)

    id = data["id"]
    name = data["name"]
    height = data["height"]
    weight = data["weight"]
    sprite_url = data["sprites"]["other"]["official-artwork"]["front_default"]

    display_string = f"id: {id} \nname: {name} \nweight: {weight} \nheight: {height} \nsprite_url: {sprite_url}"

    print (display_string)

    for i in range (len(data["types"])):
        pokemon_types = data["types"][i]["type"]["name"]
        print(f"type {i}:", pokemon_types)

    for i in range (len(data["stats"])):
        stat_name = data["stats"][i]["stat"]["name"]
        pokemon_stats = data["stats"][i]["base_stat"]
        print(f"{stat_name}:", pokemon_stats)
        i=+1

    for i in range (len(data["moves"])):
        move_name = data["moves"][i]["move"]["name"]
        print(f"move {i}:", move_name)
        if i >= 5:
            break

# # set pokemon id number
pokemon_ids = range(1, 151 + 1)

for pokemon_id in pokemon_ids:
    # call the get_pokemon_data function
    response = get_pokemon_data(pokemon_id)

    # save_pokemon_data(content_type)
    save_pokemon_data(pokemon_id, response)

    # call the print_pokemon function
    # print_pokemon(pokemon_id)