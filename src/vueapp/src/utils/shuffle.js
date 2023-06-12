export function shuffleArray(array) {
  for (let i = array.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1))
    ;[array[i], array[j]] = [array[j], array[i]]
  }

  console.log(isSolvable(array))
  while (!isSolvable(array)) {
    shuffleArray(array)
  }

  return array
}

function isSolvable(array) {
  let inversions = 0
  const length = array.length

  for (let i = 0; i < length; i++) {
    for (let j = i + 1; j < length; j++) {
      if (array[i] > array[j] && array[i] !== 0 && array[j] !== 0) {
        inversions++
      }
    }
  }

  if (length % 2 === 0) {
    const zeroRowFromBottom = Math.floor(array.indexOf(0) / 4) + 1
    return (inversions + zeroRowFromBottom) % 2 === 0
  } else {
    return inversions % 2 === 0
  }
}
