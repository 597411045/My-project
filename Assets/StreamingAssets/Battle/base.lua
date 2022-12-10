base = {
    a = 0
}
function base:new(b)
    local o = {}
    setmetatable(o, self)
    self.__index = self;
    b = b or 0;
    self.a = b * b;
    return o;
end
function base:funcA()
    print('base' .. self.a);
end
---------------------------------------
