const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const AvatarSchema = new Schema({
    name: {
        type: String,
        required: true,
    },
    userId: {
        type: Schema.Types.ObjectId,
        ref: "users",
    },
});

module.exports = mongoose.model("avatars", AvatarSchema);