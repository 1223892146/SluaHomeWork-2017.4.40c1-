--if not UnityEngine.GameObject then
	--error("Click Make/All to generate lua wrap file")
--end

local class = {}

print("成功")

function main()
    require("ClassInit")
    local c = GameObject("test")
    print(c)
    
    return class
    
end
function  class:Start()
   print("Start运行成功")
end

function class:update()
  print("update运行成功")
end

function class:fixUpdate()
  print("Fixupdate运行完成")
end