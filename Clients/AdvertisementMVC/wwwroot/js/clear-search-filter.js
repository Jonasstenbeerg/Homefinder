(function () {
  const form = document.querySelector('form[name="searchForm"]');
  const addressInput = form.querySelector('input[name="address"]');
  const minPriceInput = form.querySelector('input[name="minPrice"]');
  const maxPriceInput = form.querySelector('input[name="maxPrice"]');
  
  const clearButton = document.querySelector('#clearButton');
  clearButton.addEventListener('click', () => {
    addressInput.value = '';
    minPriceInput.value = 'min';
    maxPriceInput.value = 'max';
    
    form.submit();
  })  
})();