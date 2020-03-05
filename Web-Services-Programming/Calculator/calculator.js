function calculate() {
    let equation =  document.getElementById("equation").value;
    let result = eval(equation);
    let answer = document.getElementById("answer");
    answer.value = result;
}

document.addEventListener('click', function(event) {
    let value = event.target.innerText;
    let result = document.getElementById("result");
    if(value !== "=") {
        result.value += value;
    } else {
        result.value = eval(result.value);
    }
    
}, false);