const btnFetchCustomer: HTMLInputElement = <HTMLInputElement>document.getElementById("btnFetchCustomer");
const txtCustomerId:    HTMLInputElement = <HTMLInputElement>document.getElementById("txtCustomerId");
const txtName:          HTMLInputElement = <HTMLInputElement>document.getElementById("txtName");
const txtZipCode:       HTMLInputElement = <HTMLInputElement>document.getElementById("txtZipCode");
const txtCity:          HTMLInputElement = <HTMLInputElement>document.getElementById("txtCity");
const txtRating:        HTMLInputElement = <HTMLInputElement>document.getElementById("txtRating")
const errorCustomerId:  HTMLElement      = <HTMLElement>document.getElementById("errorCustomerId")

function displayCustomer(customer: Customer) {
  //
  // TODO
  //
}

function setValidationError(inputElement: HTMLInputElement, errorMessage: string) {
  inputElement.classList.add("is-invalid");
  errorCustomerId.innerHTML = errorMessage;
  txtName.value = "";
  txtZipCode.value = "";
  txtCity.value = "";
  txtRating.value = "";
}

function resetValidationError(inputElement: HTMLInputElement) {
  inputElement.classList.remove("is-invalid");
  errorCustomerId.innerHTML = "";
}

function onFetchButtonClicked() {
  let customerId = txtCustomerId.value;
  resetValidationError(txtCustomerId);

  //
  // TODO
  //
}

window.onload = () => btnFetchCustomer.onclick = onFetchButtonClicked
