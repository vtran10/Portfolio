window.onload = function () {
  // Initialize with Pokemon ID 1
  pokemonData().initPokemonData(2);
};

function pokemonData() {
  return {
    pokemon: { id: "150", name: "MewTwo", weight: "99", height: "99" }, // Assuming pokemon is an object with a name property
    previousPokemonName: "Dragonite",
    previousPokemonId: 149,
    nextPokemonName: "Bulbasaur",
    nextPokemonId: 1,
    

    async initPokemonData(pokemonId) {
      // Initialize with Pokemon objects
      let pokemon,
        pokemonS,
        pokemonC = {};
      // console.clear();

      if (pokemonId < 1) {
        pokemonId = 151;
        console.log("pokemonId was < 1 pokemonId: ", pokemonId);
      }

      try {
        // Fetch data for pokemonData
        let response = await axios.get(
          `/pokemon_data/pokemon_${pokemonId}_data.json`
        );
        let data = response.data;
        pokemon = data;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      try {
        // Fetch data for pokemonSData
        response = await axios.get(
          `/pokemon_sdata/pokemon_${pokemonId}_sdata.json`
        );
        data = response.data;
        pokemonS = data;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      try {
        // Fetch data for pokemonCData
        let j = pokemonS.evolution_chain.url.split("/")[6];
        response = await axios.get(`/pokemon_cdata/pokemon_${j}_cdata.json`);
        data = response.data;
        pokemonC = data;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      try {
        // Fetch data for previous pokemon
        let prevPokemonId = pokemonId;
        prevPokemonId = prevPokemonId - 1;
        if (prevPokemonId < 1) {
          prevPokemonId = 151;
        }
        response = await axios.get(
          `/pokemon_data/pokemon_${prevPokemonId}_data.json`
        );
        data = response.data;
        previousPokemon = data;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      try {
        // Fetch data for next pokemon
        let nextPokemonId = pokemonId + 1;
        if (nextPokemonId > 151) {
          nextPokemonId = 1;
        }
        response = await axios.get(
          `/pokemon_data/pokemon_${nextPokemonId}_data.json`
        );
        data = response.data;
        nextPokemon = data;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      try {
        // fetch image data
        imageUrl = `../frontend/pokemon_images/pokemon_${pokemonId}.png`;
      } catch (error) {
        console.error(`Error: ${error.message}`);
      }

      this.pokemon = pokemon; // Update the pokemon property with this instance's data
      this.pokemonS = pokemonS; // Update the pokemonS property with this instance's data
      this.pokemonC = pokemonC; // Update the pokemonC property with this instance's data
      this.previousPokemon = previousPokemon; // Update the previousPokemon property with this instance's data
      this.nextPokemon = nextPokemon; // Update the nextPokemon property with this instance's data
      this.previousPokemonName = previousPokemon.name;
      this.previousPokemonId = previousPokemon.id;
      this.nextPokemonName = nextPokemon.name;
      this.nextPokemonId = nextPokemon.id;
      this.imageUrl = imageUrl;

      // console.log("imageUrl: ", imageUrl);
      // console.log("pokemonId: ", pokemonId);
      // console.log("this.previousPokemon: ", this.previousPokemon.name);
      // console.log("previousPokemon id: ", this.previousPokemon.id);
      // console.log("this.nextPokemon: ", this.nextPokemon.name);
      // console.log("nextPokemon id: ", this.nextPokemon.id);
      // console.log("pokemon: ", pokemon);
      // console.log("pokemonS: ", pokemonS);
      // console.log("pokemonC: ", pokemonC);
    },

    refreshPage() {
      let newPokemonId = getRandomNumber(1, 152);
      this.initPokemonData(newPokemonId);
      console.log("Refreshed page with Pokemon ID:", newPokemonId);
    },

    getPreviousPokemon() {
      let newPokemonId = this.pokemon.id;
      if (newPokemonId < 1) {
        newPokemonId = 151;
      }
      newPokemonId = newPokemonId - 1;
      this.initPokemonData(newPokemonId);
      console.log("Refreshed page with Pokemon ID:", newPokemonId);
      // console.log("previousPokemon: ", previousPokemon);
      // console.log("this.previousPokemon: ", this.previousPokemon);
      // console.log("nextPokemon: ", nextPokemon);
      // console.log("this.nextPokemon: ", this.nextPokemon);
    },

    getNextPokemon() {
      let newPokemonId = this.pokemon.id + 1;
      if (newPokemonId > 151) {
        newPokemonId = 1;
        console.log("newPokemonId was > 151 newPokemonId: ", newPokemonId);
      }
      this.initPokemonData(newPokemonId);
      console.log("Refreshed page with Pokemon ID:", newPokemonId);
      // console.log("previousPokemon: ", previousPokemon);
      // console.log("this.previousPokemon: ", this.previousPokemon);
      // console.log("nextPokemon: ", nextPokemon);
      // console.log("this.nextPokemon: ", this.nextPokemon);
    },
  };
}
