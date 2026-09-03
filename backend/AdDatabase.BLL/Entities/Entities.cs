namespace AdDatabase.BLL.Entities;
public abstract class Entity { public int Id { get; set; } }
public class Role:Entity { public string Name {get;set;}=""; public ICollection<User> Users {get;set;}=[]; }
public class User:Entity { public string Email {get;set;}=""; public string PasswordHash {get;set;}=""; public string UserName {get;set;}=""; public string Status {get;set;}="Active"; public int RoleId {get;set;} public Role? Role {get;set;} public Profile? Profile {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class Profile:Entity { public int UserId {get;set;} public User? User {get;set;} public string? FullName {get;set;} public string? Phone {get;set;} public string? ImageUrl {get;set;} public string? Bio {get;set;} }
public class Session:Entity { public int UserId {get;set;} public string RefreshToken {get;set;}=""; public DateTime ExpiresAt {get;set;} public DateTime? RevokedAt {get;set;} }
public class VerificationCode:Entity { public int UserId {get;set;} public string Code {get;set;}=""; public string Type {get;set;}="Email"; public DateTime ExpiresAt {get;set;} public bool IsUsed {get;set;} }
public class City:Entity { public string Name {get;set;}=""; public ICollection<Region> Regions {get;set;}=[]; }
public class Region:Entity { public string Name {get;set;}=""; public int CityId {get;set;} public City? City {get;set;} }
public class Category:Entity { public string Name {get;set;}=""; public int? ParentCategoryId {get;set;} public Category? Parent {get;set;} public ICollection<Category> Children {get;set;}=[]; }
public class Ad:Entity { public int UserId {get;set;} public User? User {get;set;} public int CategoryId {get;set;} public Category? Category {get;set;} public int RegionId {get;set;} public Region? Region {get;set;} public string Title {get;set;}=""; public string Description {get;set;}=""; public decimal Price {get;set;} public string Status {get;set;}="Pending"; public DateTime CreatedAt {get;set;}=DateTime.UtcNow; public DateTime? ExpiresAt {get;set;} public ICollection<AdImage> Images {get;set;}=[]; public ICollection<AdAttribute> Attributes {get;set;}=[]; public ICollection<AdTag> AdTags {get;set;}=[]; }
public class AdImage:Entity { public int AdId {get;set;} public Ad? Ad {get;set;} public string Url {get;set;}=""; public int SortOrder {get;set;} public bool IsPrimary {get;set;} }
public class AdAttribute:Entity { public int AdId {get;set;} public Ad? Ad {get;set;} public string Key {get;set;}=""; public string Value {get;set;}=""; }
public class Tag:Entity { public string Name {get;set;}=""; public ICollection<AdTag> AdTags {get;set;}=[]; }
public class AdTag { public int AdId {get;set;} public Ad? Ad {get;set;} public int TagId {get;set;} public Tag? Tag {get;set;} }
public class Business:Entity { public int UserId {get;set;} public int RegionId {get;set;} public string Name {get;set;}=""; public string? Description {get;set;} public bool IsVerified {get;set;} }
public class BusinessCategory:Entity { public int BusinessId {get;set;} public int CategoryId {get;set;} }
public class BusinessItem:Entity { public int BusinessId {get;set;} public string Name {get;set;}=""; public string? Description {get;set;} public decimal Price {get;set;} public bool IsUpgradeToAd {get;set;} public ICollection<ItemImage> Images {get;set;}=[]; }
public class ItemImage:Entity { public int BusinessItemId {get;set;} public BusinessItem? BusinessItem {get;set;} public string Url {get;set;}=""; public int SortOrder {get;set;} }
public class Conversation:Entity { public int AdId {get;set;} public int BuyerUserId {get;set;} public int SellerUserId {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; public DateTime? LastMessageAt {get;set;} public ICollection<Message> Messages {get;set;}=[]; }
public class Message:Entity { public int ConversationId {get;set;} public Conversation? Conversation {get;set;} public int SenderUserId {get;set;} public string Body {get;set;}=""; public bool IsRead {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; public ICollection<MessageAttachment> Attachments {get;set;}=[]; }
public class MessageAttachment:Entity { public int MessageId {get;set;} public Message? Message {get;set;} public string Url {get;set;}=""; public string ContentType {get;set;}=""; }
public class Comment:Entity { public int AdId {get;set;} public int UserId {get;set;} public string Body {get;set;}=""; public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class Rating:Entity { public int UserId {get;set;} public int RatedUserId {get;set;} public byte Value {get;set;} public string? Comment {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class AdFavorite { public int UserId {get;set;} public int AdId {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class Follow { public int FollowerUserId {get;set;} public int FollowedUserId {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class News:Entity { public string Title {get;set;}=""; public string Body {get;set;}=""; public DateTime PublishedAt {get;set;}=DateTime.UtcNow; }
public class Notification:Entity { public int UserId {get;set;} public string Type {get;set;}=""; public string Message {get;set;}=""; public bool IsRead {get;set;} public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class Report:Entity { public int AdId {get;set;} public int ReporterUserId {get;set;} public string Reason {get;set;}=""; public string Status {get;set;}="Open"; }
public class Request:Entity { public int UserId {get;set;} public string Type {get;set;}=""; public string Details {get;set;}=""; public string Status {get;set;}="Pending"; }
public class SearchHistory:Entity { public int UserId {get;set;} public string Query {get;set;}=""; public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }
public class Verification:Entity { public int UserId {get;set;} public int? BusinessId {get;set;} public string DocumentUrl {get;set;}=""; public string Status {get;set;}="Pending"; }
