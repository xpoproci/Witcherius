function ChangeAttributes(school, race) {
    var races = [[1.8, 1.8, 1.8], [2.2, 1.6, 1.6], [1.7, 2, 2]];

    var schools = [[1.8, 1.8, 1.8], [1.5, 2, 2], [2, 1.7, 1.7], [1.9, 1.9, 1.7], [1.7, 1.8, 2]];

    var attributes = [1, 1, 1];
    var base = [10, 10, 15];

    for (var i = 0; i < attributes.length; i++) {
        attributes[i] = base[i] * schools[parseInt(school)][i];
    }
    for (var j = 0; j < attributes.length; j++) {
        attributes[j] = attributes[j] * races[parseInt(race)][j];
    }
    document.getElementById("att").style.width = ((attributes[0] / 80) * 100 + "%");
    document.getElementById("def").style.width = ((attributes[1] / 80) * 100 + "%");
    document.getElementById("hp").style.width = ((attributes[2] / 80) * 100 + "%");
}

function ChangeImage(selectBox) {
    var races = ["human", "elf", "dwarf"];
    var schools = ["wolf", "bear", "cat", "viper", "griffin"];
    var bp = "../Content/images/chars/";
    var img = document.getElementById("avatar");
    var race = document.getElementById("Race");
    var school = document.getElementById("School");
    img.fadeOut();
    img.src = bp + races[race.value] + "_" + schools[school.value] + ".jpg";
    img.fadeIn();
    ChangeAttributes(school.value, race.value);
}