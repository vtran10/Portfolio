function delay(milliseconds) {
  return new Promise((resolve) => {
    setTimeout(() => {
      console.log(`Waited for ${milliseconds} milliseconds`);
      resolve(); // Resolves the Promise after the specified delay
    }, milliseconds);
  });
}

// // Usage
// delay(1000)
//   .then(() => {
//     console.log("After 1 second");
//     // Code to execute after the delay
//   })
//   .catch((error) => {
//     console.error("Error:", error);
//   });
