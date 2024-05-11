const screenHeight = document.documentElement.clientHeight;
const header = document.querySelector('.header');
const contentHeight = screenHeight - header.offsetHeight;

const contentElement = document.querySelector('.warehouse-content');
contentElement.style.height = `${contentHeight}px`;

const warehouseSortPanelElement = document.querySelector('.warehouse-control-panel');
const warehouseTableElement = document.querySelector('.warehouse-table');
warehouseTableElement.style.height = `${contentHeight - warehouseSortPanelElement.offsetHeight}px`;