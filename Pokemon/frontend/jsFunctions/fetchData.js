// function to load pokemon card data
async function fetchData() {
  console.clear();
  // get random number between 1 and 151
  i = await getRandomNumber(1, 152);
  let c = 0;
  while (i === i && c < 10) {
    i = getRandomNumber(1, 152);
    c++;
    // console.log("i: ", i);
  }
  // i = 1;

  fetchSpeciesData(i);

  fetch(`../pokemon_data/pokemon_${i}_data.json`)
    .then((response) => response.json())
    .then((data) => {
      pokemon = data;

      // pokemon name
      let name = data.name;
      nameCapitalized = name.charAt(0).toUpperCase() + name.slice(1);
      document.getElementById("pName2").innerHTML = nameCapitalized;

      // pokemon hp stat
      const hp = pokemon.stats[0].base_stat;
      // document.getElementById("hp").innerHTML = hp;
      document.getElementById("hp2").innerHTML = hp;

      // pokemon type
      let types = [];
      const typesData = data.types; // Assign data.types to typesData

      let typeSymbol = document.getElementById("typeSymbol");
      typeSymbol.innerHTML = ""; // Clear existing content

      for (let i = 0; i < typesData.length; i++) {
        const typeName = typesData[i].type.name;

        types.push(typeName);
        typeSymbol.innerHTML += `<img src="../frontend/images/${typeName}.png" alt="${typeName}" width="30px" height="30px">`;
      }

      // pokemon image
      imageUrl = `../frontend/pokemon_images/pokemon_${i}.png`;
      document.getElementById("imageUrl2").src = imageUrl;
      document.getElementById("imageUrl2").className = "rounded-xl my-[-1rem] p-2 object-contain mx-auto border-2 border-gray-300 bg-transparent";  
      console.log("imageUrl2: ", imageUrl);

      // pokemon statsLeft column
      statsLeft = data.stats;
      const statsLeftContainer = document.getElementById("statsLeft");
      statsLeftContainer.innerHTML = ""; // Clear existing content
      statsLeft.forEach((stat) => {
        const statElement = document.createElement("p");
        statElement.innerHTML = `${stat.stat.name}: ${stat.base_stat}`;
        statsLeftContainer.appendChild(statElement);
      });

      // pokemon statsRight column, divide by value by 10
      // as unit of measures are height = decimeter and weight = hectogram
      statsRight = [
        { key: "height", value: `${data.height / 10} m` },
        { key: "weight", value: `${data.weight / 10} kg` },
      ];
      const statsRightContainer = document.getElementById("statsRight");
      statsRightContainer.innerHTML = ""; // Clear existing content
      statsRight.forEach((stat) => {
        const statElement = document.createElement("p");
        statElement.innerHTML = `${stat.key}: ${stat.value}`;
        statsRightContainer.appendChild(statElement);
      });

      // pokemon types
      types = data.types;
      const typesContainer = document.getElementById("types");
      typesContainer.innerHTML = ""; // Clear existing content
      types.forEach((type) => {
        const typeElement = document.createElement("p");
        typeElement.innerHTML = `type: ${type.type.name}`;
        typesContainer.appendChild(typeElement);
      });
    })
    .catch((error) => {
      console.error("Error fetching data:", error);
    });
}
