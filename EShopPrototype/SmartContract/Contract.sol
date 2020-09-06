pragma solidity 0.7.1;

contract EShop
{
    mapping(uint256 => uint256) public products;
    
    event LogPricechange(uint256 productId, uint256 price);

    function AddOrUpdateProduct(uint256 productId, uint256 price) external
    {
        products[productId] = price;
        emit LogPricechange(productId, price);
    }
}