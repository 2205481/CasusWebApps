const imageSelector = document.getElementById('imageSelector');
const canvas = document.getElementById('imageCanvas');
const ctx = canvas.getContext('2d');
const image = document.getElementById('image');
const tagNameInput = document.getElementById('tagName');
const annotations = [];

imageSelector.addEventListener('change', function () {
    loadImage();
});

function drawImage() {
    ctx.drawImage(image, 0, 0, image.width, image.height);
}

function loadImage() {
    const selectedImageId = imageSelector.value;

    if (!selectedImageId) {
        return;
    }

    const imageUrl = '/ImageUpload/GetImageById?id=' + selectedImageId;

    image.onload = function () {
        canvas.width = image.width;
        canvas.height = image.height;
        drawImage();
        canvas.style.display = 'block';
    };

    image.src = imageUrl;
}

function saveAnnotation() {
    const boundingBox = getBoundingBox();
    const itemTypeElement = document.getElementById('itemType');

    if (!itemTypeElement) {
        console.error('itemType element not found');
        return;
    }

    const itemType = itemTypeElement.value;

    // Create FormData object and append annotation data
    const formData = new FormData();
    formData.append('ItemType', itemType);
    formData.append('BoundingBoxX', boundingBox.x);
    formData.append('BoundingBoxY', boundingBox.y);
    formData.append('BoundingBoxWidth', boundingBox.width);
    formData.append('BoundingBoxHeight', boundingBox.height);

    // Send annotations to the server using AJAX
    fetch('ImageUpload/SaveAnnotation', {
        method: 'POST',
        body: formData,
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to save annotation');
            }
            console.log('Annotation saved successfully');
        })
        .catch(error => {
            console.error(error);
        });
}


function getBoundingBox() {
    const x = 50;
    const y = 50;
    const width = 100;
    const height = 100;

    return { x, y, width, height };
}

let isDrawing = false;
let startPoint = {};
let endPoint = {};

canvas.addEventListener('mousedown', function (e) {
    isDrawing = true;
    startPoint = { x: e.clientX - canvas.getBoundingClientRect().left, y: e.clientY - canvas.getBoundingClientRect().top };
});

canvas.addEventListener('mousemove', function (e) {
    if (!isDrawing) return;
    endPoint = { x: e.clientX - canvas.getBoundingClientRect().left, y: e.clientY - canvas.getBoundingClientRect().top };

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    drawImage();
    ctx.strokeRect(startPoint.x, startPoint.y, endPoint.x - startPoint.x, endPoint.y - startPoint.y);
});

canvas.addEventListener('mouseup', function () {
    isDrawing = false;
    saveAnnotation();
});