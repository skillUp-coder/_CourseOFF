var entrancetext = document.getElementById('entranceText');
entrancetext.innerHTML = 'Entrance.';

var LogInText = document.getElementById('LogInText');
LogInText.innerHTML = 'Log In.';

var bodyButton = document.getElementById('CreateButton');
var createButton = document.createElement('button');
bodyButton.appendChild(createButton);

createButton.setAttribute('class', 'btn btn-primary btn-block');
createButton.innerHTML = 'Log In';

