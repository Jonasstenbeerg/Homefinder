(function () {
var arrow = document.getElementById("filter-arrow");
var container = document.querySelector(".search__container");

console.log("test");

arrow.addEventListener("click", function() {
  container.style.display = container.style.display === "none" ? "block" : "none";
  arrow.style.transform = arrow.style.transform === "rotate(180deg)" ? "rotate(0)" : "rotate(180deg)";
});
})();