// Function to generate a random integer in a specific range
function getRandomNumber(min, max) {
  number = Math.floor(Math.random() * (max - min + 1)) + min;
  return number;
}