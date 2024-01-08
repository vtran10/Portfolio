// Function to fetch data from the source
async function initPokemonData(num) {
  // console.clear();
  try {
    // if initPokemonData is called without a parameter, set num to a random number
    if (num === undefined) {
      num = getRandomNumber(2 , 2);
    }
    i = num; // Access the 'i' value from Alpine.js state
    // console.log("i: ", i);
    // i = 1;
    
    // Fetch data for pokemonData
    let response = await axios.get(`../pokemon_data/pokemon_${i}_data.json`);
    let data = response.data;
    // Set the fetched data in Alpine.js state
    this.p = data;
    p = data;
    this.name = p.name;
    this.weight = p.weight;
    this.height = p.height;
    this.id = p.id;
    // console.log("p: ", p);

    // Fetch data for pokemonSData
    response = await axios.get(`../pokemon_sdata/pokemon_${i}_sdata.json`);
    data = response.data;
    // Set the fetched data in Alpine.js state
    this.s = data;
    s = data;
    if (data.evolves_from_species === null) {
      this.evoFrom == null;
    } else {
      this.evoFrom = s.evolves_from_species.name;
    }
    // this.evoFrom = s.evolves_from_species.name;
    evoFrom = this.evoFrom;
    this.evoChain = s.evolution_chain.url.split("/")[6];
    evoChain = this.evoChain; // required for j
    // console.log("s: ", s);
    // console.log("evoFrom: ", evoFrom);
    // console.log('evoChain:', evoChain);

    //assign j as the url split = pokemonSData.pokemon.evolution_chain.url.split('/')[6]
    let j = evoChain;
    // console.log('j: evo-chain', j);

    // Fetch data for pokemonCData
    response = await axios.get(`../pokemon_cdata/pokemon_${j}_cdata.json`);
    data = response.data;
    // Set the fetched data in Alpine.js state
    this.c = data;
    c = data;
    this.evo1 = c.chain.evolves_to[0]?.species.name;
    this.evo2 = c.chain.evolves_to[0]?.evolves_to[0]?.species.name;
    evo1 = this.evo1;
    evo2 = this.evo2;
    // console.log("c: ", c);
    // console.log("evo1: ", evo1);
    // console.log("evo2: ", evo2);

    // Fetch data for prevPokemon
    let k = i; // index for nextPokemon
    // console.log("i before if: ", i);
    if (i <= 1) {
      i = i;
      // console.log("i < 1: ", i);
    } else {
      i = i - 1;
      // console.log("i > 1: ", i);
    }
    // console.log("i after if: ", i);
    response = await axios.get(`../pokemon_data/pokemon_${i}_data.json`);
    data = response.data;
    // Set the fetched data in Alpine.js state
    this.prevPokemon = data;
    prevPokemon = data;
    this.prevPokemonId = prevPokemon.id;
    this.prevPokemonName = prevPokemon.name;
    // prevPokemonId = prevPokemon.id;
    // console.log("prevPokemon:", prevPokemon);

    // Fetch data for nextPokemon
    // console.log("i after if statement: ", i);
    // console.log("k after if statement: ", k);
    k = k + 1; // Access the 'i' value from Alpine.js state
    response = await axios.get(`../pokemon_data/pokemon_${k}_data.json`);
    data = response.data;
    // Set the fetched data in Alpine.js state
    this.nextPokemon = data;
    nextPokemon = data;
    this.nextPokemonName = nextPokemon.name;
    // console.log("nextPokemon:", nextPokemon);

    // console.log("p.name: ", p.name);
    // console.log("p.id: ", p.id);
    // console.log("s.chain: ", s.evolution_chain.url);
    // console.log("c.id: ", c.id);
  } catch (error) {
    // console.error(`Error: ${error.message}`);
    console.log("error: ", error);
  }
}
