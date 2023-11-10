const imageSelector = document.getElementById('imageSelector');
const canvas = document.getElementById('imageCanvas');
const ctx = canvas.getContext('2d');
const image = document.getElementById('image');
const tagNameInput = document.getElementById('tagName');
let itemTypeElement;
const annotations = [];

imageSelector.addEventListener('change', function () {
    loadImage();
});

function drawImage() {
    ctx.drawImage(image, 0, 0, image.width, image.height);

    const boundingBox = getBoundingBox();

    ctx.fillStyle = '#FFFFFF';
    ctx.fillRect(startPoint.x, startPoint.y - 25, ctx.measureText(itemTypeElement.value).width, 25);

    ctx.strokeStyle = '#00FF00';
    ctx.lineWidth = 8;
    ctx.strokeRect(boundingBox.x, boundingBox.y, boundingBox.width, boundingBox.height);

    ctx.strokeRect(startPoint.x, startPoint.y,
        endPoint.x - endPoint.x, endPoint.y - startPoint.y);

    const itemType = itemTypeElement.value;
    ctx.font = '20px Arial';
    ctx.fillStyle = '#000000';
    ctx.fillText(itemType, startPoint.x, startPoint.y - 5);
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

    itemTypeElement = document.getElementById('itemType');
}

function saveAnnotation() {
    const boundingBox = getBoundingBox();
    const itemTypeElement = document.getElementById('itemType');

    if (!itemTypeElement) {
        console.error('itemType element not found');
        return;
    }

    const itemType = itemTypeElement.value;
    const canvasImageData = canvas.toDataURL('image/jpeg');

    // Create FormData object and append annotation data
    const formData = new FormData();
    formData.append('ItemType', itemType);
    formData.append('CanvasImage', canvasImageData);
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
    const x = startPoint.x;
    const y = startPoint.y;
    const width = endPoint.x - startPoint.x;
    const height = endPoint.y - startPoint.y;

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