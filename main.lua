buttons = {}
-- menu section
debugText = ""
inMenu = true
moveMenuUp = false
moveMenuDown = false
currentButton = 1
-- /menu section
function love.draw()
    love.graphics.print(debugText, 5, 5)
    love.graphics.print("Jam Game", centerX - 40, centerY - 60)
    for i, button in pairs(buttons) do
        love.graphics.print(button.text, button.x, button.y)
        if i == currentButton then
            vertices = {button.x - 5, button.y - 5, button.x + 105, button.y - 5, button.x + 105, button.y + 35, button.x - 5, button.y + 35}
            love.graphics.polygon("line", vertices)
        end
    end
end

function love.update(dt)
    if inMenu then
        handleMenuInput()
    end
end

function handleMenuInput()
    if moveMenuUp then
        currentButton = currentButton - 1
        if currentButton < 1 then
            currentButton = #buttons
        end
        moveMenuUp = false
    end
    if moveMenuDown then
        currentButton = currentButton + 1
        if currentButton > #buttons then
            currentButton = 1
        end
        moveMenuDown = false
    end
end

function love.keypressed(key)
    if (key == "up" and not moveMenuDown) then
       moveMenuUp = true
    end
    if (key == "down" and not moveMenuUp) then
        moveMenuDown = true
    end
end

function love.keyreleased(key)
end

function love.focus(f)
    if not f then
      print("LOST FOCUS")
      if currentMusic:isPlaying() then
        currentMusic:pause()
      end
    else
      print("GAINED FOCUS")
      if not currentMusic:isPlaying() then
        love.audio.play(currentMusic)
      end
    end
end

function love.load()
    menuMusic = love.audio.newSource("audio/menu.ogg", "stream", true)
    menuMusic:setVolume(0.75)
    --menuMusic:setPitch(0.75)
    menuMusic:play()
    currentMusic = menuMusic

    font = love.graphics.newFont("font/Cyberphont.ttf", 15)
    love.graphics.setFont(font)

    centerX = love.graphics.getWidth() / 2
    centerY = love.graphics.getHeight() / 2
    buttons[1] = { x = 40, y = centerY, text = "New Game"}
    buttons[2] = { x = 40, y = centerY + 60, text = "Credits"}
    buttons[3] = { x = 40, y = centerY + 120, text = "Quit"}
end

function love.quit()
    print("Thanks for playing! Come back soon!")
end
