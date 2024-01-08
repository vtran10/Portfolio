// function to load pokemon chain data
async function fetchSpeciesData(num) {
  //get random pokemon species number, spokemon = species pokemon
  let i = num;

  // fetch pokemon species data
  fetch(`../pokemon_sdata/pokemon_${i}_sdata.json`)
    .then((response) => response.json())
    .then((sdata) => {
      spokemon = sdata;
      calledPokemon = spokemon.name;
      console.log("spokemon: ", spokemon);

      // fetch pokemon evolution chain data
      j = spokemon.evolution_chain.url.match(/\/(\d+)\/$/)[1];
      // console.log("j: ", j);

      fetch(`../pokemon_cdata/pokemon_${j}_cdata.json`)
        .then((response) => response.json())
        .then((cdata) => {
          cpokemon = cdata;
          console.log("cpokemon: ", cpokemon);

          // get baby pokemon name/id
          const baby = cpokemon.chain.species.name;

          // get number of evolutions > each evolves_to array
          const evo1 = cpokemon.chain.evolves_to[0]?.species.name;
          const evo2 =
            cpokemon.chain.evolves_to[0]?.evolves_to[0]?.species.name;
          const evo3 =
            cpokemon.chain.evolves_to[0]?.evolves_to[0]?.evolves_to[0]?.species
              .name;

          // create if statement to check if pokemon has evolutions
          if (evo3 !== undefined) {
            console.log(
              `this pokemon has 3 evolutions. ${evo1} and ${evo2} and ${evo3}`
            );
          } else if (evo2 !== undefined) {
            console.log(
              `this pokemon has 2 evolutions. ${baby}, > ${evo1} > ${evo2}`
            );
          } else if (evo1 !== undefined) {
            console.log(`this pokemon has 1 evolution. ${evo1}`);
          } else {
            console.log(`this pokemon has no evolution. ${evo1}`);
          }

          console.log(
            `Info: i: ${i}, pokemon: ${calledPokemon}  baby: ${baby}, evo1: ${evo1}, evo2: ${evo2}, evo3: ${evo3}`
          );

          // console.log(`calledPokemon: ${calledPokemon} baby: ${baby}`);

          if (calledPokemon === baby) {
            console.log("inside calledPokemon === baby");
            // Check if evolvesFromDiv exists
            const evolvesFromDiv = document.getElementById("evolvesFrom");
            containerTopDiv = document.getElementById("containerTop");
            containerTopDiv.style.display = "flex";

            if (evolvesFromDiv) {
              // Clear the existing content of the evolvesFromDiv
              evolvesFromDiv.innerHTML = "";

              // Create a new paragraph element
              const pElement = document.createElement("p");
              pElement.className =
                "text-center text-sm text-black font-mono text-nowrap";

              // Create an i element with a class of "bg-gray-200 rounded px-2"
              const iElement = document.createElement("i");
              iElement.className = "bg-gray-200 rounded px-2";
              iElement.textContent = "Basic";

              // Append the i element to the paragraph element
              pElement.appendChild(iElement);

              // Create a new div for the image
              const imageDiv = document.createElement("div");

              imageDiv.style.display = "";
              imageDiv.id = "imageUrl3";
              imageDiv.className =
                "absolute bg-gray-200 p-0.5 rounded-xl top-6 hidden";

              // Create an img element
              const imgElement = document.createElement("img");
              imgElement.className = "rounded-xl";
              imgElement.src = `../frontend/pokemon_images/pokemon_${i}.png`;
              imgElement.alt = "pokemon image";
              imgElement.onerror =
                "this.src='../frontend/images/questionMark.png'";
              width = "50px";
              height = "50px";

              // Append the img element to the image div
              imageDiv.appendChild(imgElement);

              // Append the paragraph and image divs to the evolvesFromDiv
              evolvesFromDiv.appendChild(pElement);
              evolvesFromDiv.appendChild(imageDiv);

              // Append the evolvesFromDiv to the document body or any other container element
              const appendHereDiv = document.getElementById("appendHere");
              appendHereDiv.appendChild(evolvesFromDiv);

              // append the evolvesFromDiv2 to the document body or any other container element
              const divEvolvesFrom = document.getElementById("divEvolvesFrom");
              divEvolvesFrom.style.display = "";

              const evolvesFrom2Div = document.getElementById("evolvesFrom2");
              spanElement = document.getElementById("pName3");
              spanElement.innerText = `${baby}`;
            }
          } else if (calledPokemon !== baby) {
            console.log("inside calledPokemon !== baby");
            // Check if evolvesFromDiv exists
            const evolvesFromDiv = document.getElementById("evolvesFrom");
            containerTopDiv = document.getElementById("containerTop");
            containerTopDiv.style.display = "flex";

            if (evolvesFromDiv) {
              // Clear the existing content of the evolvesFromDiv
              evolvesFromDiv.innerHTML = "";

              // Create a new paragraph element
              const pElement = document.createElement("p");
              pElement.className =
                "text-center text-sm text-black font-mono text-nowrap";

              // Create an i element with a class of "bg-gray-200 rounded px-2"
              const iElement = document.createElement("i");
              iElement.className = "bg-gray-200 rounded px-2 mr-2 mt-8";

              let stageNo = 0;
              if (calledPokemon === evo1) {
                stageNo = 2;
              } else if (calledPokemon === evo2) {
                stageNo = 3;
              }

              // Set the text content of iElement based on the determined stageNo
              iElement.textContent = `STAGE ${stageNo}`;

              // Append the i element to the paragraph element
              pElement.appendChild(iElement);

              // Create a new div for the image
              const imageDiv = document.createElement("div");

              imageDiv.style.display = "";
              imageDiv.id = "imageUrl3";
              imageDiv.className =
                "absolute bg-gray-200 p-0.5 rounded-xl top-6";

              // Create an img element

              // Assuming spokemon.evolution_chain.url is a valid URL string
              const evolutionChainUrl = spokemon.evolution_chain.url;
              // console.log("evolutionChainUrl: ", evolutionChainUrl);
              // Use a regular expression to match the last part of the URL (the Pokémon ID)
              const match = evolutionChainUrl.match(/\/(\d+)\/$/);
              // Check if the match is successful

              let savedPokemonChainId = "";
              if (match) {
                // Extract the Pokémon ID from the matched result
                const PokemonChainId = match[1];
                savedPokemonChainId = PokemonChainId; // Use = for assignment
                // console.log("PokemonChainId:", PokemonChainId);
                // Now, savedPokemonChainId contains the ID of the Pokémon in the evolution chain
              } else {
                console.error(
                  "Unable to extract Pokémon ID from the evolution chain URL"
                );
              }

              let b = savedPokemonChainId; // get pokemon chain id
              console.log("b = chain id:", b);

              let form1 = cpokemon.chain.species.url;
              let form2 = cpokemon.chain.evolves_to[0]?.species.url;
              let form3 =
                cpokemon.chain.evolves_to[0]?.evolves_to[0]?.species.url;
              // console.log("form1: ", form1);

              let prevform = "";
              if (calledPokemon == evo1) {
                prevform = form1;
                console.log("calledPokemon: ", calledPokemon);
                console.log("evo1: ", evo1);
                console.log("prevform1: ", prevform);
              } else if (calledPokemon == evo2) {
                prevform = form2;
                console.log("calledPokemon: ", calledPokemon);
                console.log("evo2: ", evo2);
                console.log("prevform2: ", prevform);
              }

              // Assuming spokemon.evolution_chain.url is a valid URL string
              const pokemonNo = prevform;
              // console.log("pokemonNo: ", pokemonNo);
              // Use a regular expression to match the last part of the URL (the Pokémon ID)
              const match2 = pokemonNo.match(/\/(\d+)\/$/);
              // Check if the match is successful

              let savedPokemonNo = "";
              if (match2) {
                // Extract the Pokémon ID from the matched result
                const PokemonChainId2 = match2[1];
                savedPokemonNo = PokemonChainId2; // Use = for assignment
                console.log("savedPokemonNo:", savedPokemonNo);
                // Now, savedPokemonNo contains the ID of the Pokémon in the evolution chain
              } else {
                console.error(
                  "Unable to extract Pokémon ID from the evolution chain URL"
                );
              }

              let a = savedPokemonNo; // a = pokemon id of the previous pokemon form
              console.log("a = pokemon id of the previous pokemon form:", a);

              const imgElement = document.createElement("img");
              imgElement.className = "rounded-xl";
              imgElement.src = `../frontend/pokemon_images/pokemon_${a}.png`;
              imgElement.alt = "pokemon image";
              imgElement.onerror =
                "this.src='../frontend/images/questionMark.png'";
              width = "50px";
              height = "50px";

              // Append the img element to the image div
              imageDiv.appendChild(imgElement);

              // Append the paragraph and image divs to the evolvesFromDiv
              evolvesFromDiv.appendChild(pElement);
              evolvesFromDiv.appendChild(imageDiv);

              // Append the evolvesFromDiv to the document body or any other container element
              const appendHereDiv = document.getElementById("appendHere");
              appendHereDiv.appendChild(evolvesFromDiv);

              // append the evolvesFromDiv2 to the document body or any other container element
              const divEvolvesFrom = document.getElementById("divEvolvesFrom");
              divEvolvesFrom.style.display = "flex";

              const evolvesFrom2Div = document.getElementById("evolvesFrom2");
              spanElement = document.getElementById("pName3");
              // is called pokemon the same as evo1
              previousForm = "";
              if (calledPokemon === evo2) {
                previousForm = evo1;
                // console.log(previousForm);
              } else if (calledPokemon === evo1) {
                previousForm = baby;
                // console.log(previousForm);
              }
              console.log("previousForm: ", previousForm);

              const pName = previousForm;
              const capitalizedPName =
                pName.charAt(0).toUpperCase() + pName.slice(1);
              spanElement.innerText = `${capitalizedPName}`;
            }
          }
          console.log("enddddddddddddddddddddddddddddddddd");
        });
    });
}
