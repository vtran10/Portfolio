// Function to generate a random integer in a specific range
export default function getRandomInt(min, max) {
  let number = Math.floor(Math.random() * (max - min + 1)) + min;
  return number;
}
