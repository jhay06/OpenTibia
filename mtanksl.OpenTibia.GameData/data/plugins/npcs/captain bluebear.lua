﻿local say = topic:new()
say:add("name", "My name is Captain Bluebear from the Royal Tibia Line.")
say:add("job", "I am the captain of this sailing-ship.")
say:add("thais", "This is Thais. Where do you want to go?")
say:addtravel( {
    question = "Do you seek a passage to {town} for {price} gold?",
    yes = "Set the sails!",
    notenoughtgold = "You don't have enough money.",
    no = "We would like to serve you some time."
}, {
    { name = "carlin", town = "Carlin", position = { x = 0, y = 0, z = 7 }, price = 110 },
    { name = "ab'dendriel", town = "Ab'Dendriel", position = { x = 0, y = 0, z = 7 }, price = 130 },
    { name = "edron", town = "Edron", position = { x = 0, y = 0, z = 7 }, price = 160 },
    { name = "venore", town = "Venore", position = { x = 0, y = 0, z = 7 }, price = 170 },
    { name = "port hope", town = "Port Hope", position = { x = 0, y = 0, z = 7 }, price = 160 }
} )

local handler = npchandler:new( {
    greet = "Welcome on board, Sir {playername}.",
    busy = "One moment please {playername}. You're next in line.",
    say = say,
    farewell = "Good bye. Recommend us, if you were satisfied with our service.",
    dismiss = "Good bye. Recommend us, if you were satisfied with our service."
} )

function shouldgreet(npc, player, message) return handler:shouldgreet(npc, player, message) end
function shouldfarewell(npc, player, message) return handler:shouldfarewell(npc, player, message) end
function ongreet(npc, player) handler:ongreet(npc, player) end
function onbusy(npc, player) handler:onbusy(npc, player) end
function onsay(npc, player, message) handler:onsay(npc, player, message) end
function onfarewell(npc, player) handler:onfarewell(npc, player) end
function ondismiss(npc, player) handler:ondismiss(npc, player) end